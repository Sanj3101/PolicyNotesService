using Moq;
using PolicyNotes.Models;
using PolicyNotes.Repositories;
using PolicyNotes.Services;

public class PolicyNoteServiceTests
{
    [Fact]
    public async Task AddAsync_AddsAndReturnsNote()
    {
        var mockRepo = new Mock<IPolicyNoteRepository>();
        mockRepo.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        var service = new PolicyNoteService(mockRepo.Object);

        var result = await service.AddAsync("P123", "Sample note");

        Assert.Equal("P123", result.PolicyNumber);
        Assert.Equal("Sample note", result.Note);
        mockRepo.Verify(r => r.Add(It.IsAny<PolicyNote>()), Times.Once);
    }

    [Fact]
    public void GetAll_ReturnsNotesList()
    {
        var mockRepo = new Mock<IPolicyNoteRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<PolicyNote>
        {
            new() { Id = 1, PolicyNumber = "A1", Note = "Test1" }
        });

        var service = new PolicyNoteService(mockRepo.Object);
        var result = service.GetAll();

        Assert.Single(result);
    }
}
