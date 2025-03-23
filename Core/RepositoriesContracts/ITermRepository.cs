using Infrastructure.Models;

namespace Core.RepositoriesContracts
{
    public interface ITermRepository
    {
        Task<IEnumerable<Term>> GetAllTermsAsync();
        Task<Term> GetTermByIdAsync(Guid termId);
        Task AddTermAsync(Term term);
        Task UpdateTermAsync(Term term);
        Task DeleteTermAsync(Guid termId);
    }
}
