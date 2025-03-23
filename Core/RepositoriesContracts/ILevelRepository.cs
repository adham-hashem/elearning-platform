using Infrastructure.Models;
namespace Core.RepositoriesContracts
{
    public interface ILevelRepository
    {
        Task<IEnumerable<Level>> GetAllLevelsAsync();
        Task<Level> GetLevelByIdAsync(Guid levelId);
        Task AddLevelAsync(Level level);
        Task UpdateLevelAsync(Level level);
        Task DeleteLevelAsync(Guid levelId);
    }
}
