using PolicyNotes.Models;
using PolicyNotes.Repositories;

namespace PolicyNotes.Services
{
    public class PolicyNoteService
    {
        private readonly IPolicyNoteRepository _repository;

        public PolicyNoteService(IPolicyNoteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<PolicyNote> GetAll() => _repository.GetAll();
        public PolicyNote? GetById(int id) => _repository.GetById(id);

        public async Task<PolicyNote> AddAsync(string policyNumber, string note)
        {
            var newNote = new PolicyNote
            {
                PolicyNumber = policyNumber,
                Note = note
            };

            _repository.Add(newNote);
            await _repository.SaveAsync();
            return newNote;
        }
    }
}
