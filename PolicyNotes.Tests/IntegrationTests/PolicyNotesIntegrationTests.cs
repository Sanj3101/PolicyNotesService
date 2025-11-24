using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolicyNotes.Data;
using PolicyNotes.Models;
using System.Net;
using System.Net.Http.Json;

public class PolicyNotesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public PolicyNotesIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private HttpClient CreateClient()
    {
        return _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    s => s.ServiceType == typeof(DbContextOptions<NotesDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<NotesDbContext>(opt =>
                    opt.UseInMemoryDatabase("TestNotesDB"));
            });
        }).CreateClient();
    }

    [Fact]
    public async Task Post_CreatesNote()
    {
        var client = CreateClient();

        var note = new { policyNumber = "N1", note = "Hello" };

        var response = await client.PostAsJsonAsync("/notes", note);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var client = CreateClient();
        var response = await client.GetAsync("/notes");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_IfMissing()
    {
        var client = CreateClient();
        var response = await client.GetAsync("/notes/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        var client = CreateClient();

        var req = new { policyNumber = "P222", note = "Testing GET by ID" };
        var postResponse = await client.PostAsJsonAsync("/notes", req);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        //create test note
        var created = await postResponse.Content.ReadFromJsonAsync<PolicyNote>();
        Assert.NotNull(created);

        //fetch created note
        var getResponse = await client.GetAsync($"/notes/{created!.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var fetched = await getResponse.Content.ReadFromJsonAsync<PolicyNote>();
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched!.Id);
        Assert.Equal("P222", fetched.PolicyNumber);
    }

}
