using PolicyNotes.Models;

namespace PolicyNotes.Repositories
{
    public interface IPolicyNoteRepository
    {
        IEnumerable<PolicyNote> GetAll();
        PolicyNote? GetById(int id);
        void Add(PolicyNote note);
        Task SaveAsync();
    }
}
