using Core.Entities;
using Core.RepositoriesContracts;
using Core.Services.Contracts;

namespace Core.Services.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _userRepository;

        public ApplicationUserService(IApplicationUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }
    }
}
