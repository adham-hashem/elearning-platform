using Infrastructure.Models;

namespace Core.Services.Implementations
{
    public interface ITermService
    {
        Task<IEnumerable<Term>> GetAllTermsAsync();
        Task<Term> GetTermByIdAsync(Guid termId);
        Task AddTermAsync(Term term);
        Task UpdateTermAsync(Term term);
        Task DeleteTermAsync(Guid termId);
    }
}
