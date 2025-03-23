using Core.RepositoriesContracts;
using Core.Services.Contracts;
using Infrastructure.Models;

namespace Core.Services.Implementations
{
    public class TermService : ITermService
    {
        private readonly ITermRepository _termRepository;

        public TermService(ITermRepository termRepository)
        {
            _termRepository = termRepository;
        }

        public async Task<IEnumerable<Term>> GetAllTermsAsync()
        {
            return await _termRepository.GetAllTermsAsync();
        }

        public async Task<Term> GetTermByIdAsync(Guid termId)
        {
            return await _termRepository.GetTermByIdAsync(termId);
        }

        public async Task AddTermAsync(Term term)
        {
            await _termRepository.AddTermAsync(term);
        }

        public async Task UpdateTermAsync(Term term)
        {
            await _termRepository.UpdateTermAsync(term);
        }

        public async Task DeleteTermAsync(Guid termId)
        {
            await _termRepository.DeleteTermAsync(termId);
        }
    }
}
