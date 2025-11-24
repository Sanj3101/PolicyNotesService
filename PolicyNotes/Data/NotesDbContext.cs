using Microsoft.EntityFrameworkCore;
using PolicyNotes.Models;
using System.Collections.Generic;

namespace PolicyNotes.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

        public DbSet<PolicyNote> PolicyNotes => Set<PolicyNote>();
    }
}
