using Microsoft.EntityFrameworkCore;
using PolicyNotes.Data;
using PolicyNotes.Models;
using PolicyNotes.Repositories;
using PolicyNotes.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NotesDbContext>(options => options.UseInMemoryDatabase("PolicyNotesDB"));
builder.Services.AddScoped<IPolicyNoteRepository, PolicyNoteRepository>();
builder.Services.AddScoped<PolicyNoteService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/notes", async (PolicyNoteRequest req, PolicyNoteService service) =>
{
    var newNote = await service.AddAsync(req.PolicyNumber, req.Note);
    return Results.Created($"/notes/{newNote.Id}", newNote);
});

app.MapGet("/notes", (PolicyNoteService service) =>
{
    return Results.Ok(service.GetAll());
});

app.MapGet("/notes/{id}", (int id, PolicyNoteService service) =>
{
    var note = service.GetById(id);
    return note is null ? Results.NotFound(new { message = "Policy not found" }) : Results.Ok(note);
});

app.Run();


//DTOs
public record PolicyNoteRequest(string PolicyNumber, string Note);

public partial class Program { }
