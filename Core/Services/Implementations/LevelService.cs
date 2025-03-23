using Core.RepositoriesContracts;
using Core.Services.Contracts;
using Infrastructure.Models;

namespace Core.Services.Implementations
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;

        public LevelService(ILevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<IEnumerable<Level>> GetAllLevelsAsync()
        {
            return await _levelRepository.GetAllLevelsAsync();
        }

        public async Task<Level> GetLevelByIdAsync(Guid levelId)
        {
            return await _levelRepository.GetLevelByIdAsync(levelId);
        }

        public async Task AddLevelAsync(Level level)
        {
            await _levelRepository.AddLevelAsync(level);
        }

        public async Task UpdateLevelAsync(Level level)
        {
            await _levelRepository.UpdateLevelAsync(level);
        }

        public async Task DeleteLevelAsync(Guid levelId)
        {
            await _levelRepository.DeleteLevelAsync(levelId);
        }
    }
}
