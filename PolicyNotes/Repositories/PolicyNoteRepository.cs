using PolicyNotes.Models;
using PolicyNotes.Data;
using Microsoft.EntityFrameworkCore;


namespace PolicyNotes.Repositories
{
    
        public class PolicyNoteRepository : IPolicyNoteRepository
        {
            private readonly NotesDbContext _context;

            public PolicyNoteRepository(NotesDbContext context)
            {
                _context = context;
            }

            public IEnumerable<PolicyNote> GetAll() => _context.PolicyNotes.AsNoTracking().ToList();

            public PolicyNote? GetById(int id) => _context.PolicyNotes.AsNoTracking().FirstOrDefault(n => n.Id == id);

            public void Add(PolicyNote note)
            {
                _context.PolicyNotes.Add(note);
            }

            public async Task SaveAsync() => await _context.SaveChangesAsync();
        }
}

