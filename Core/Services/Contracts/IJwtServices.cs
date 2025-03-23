using Core.DTOs.AuthDtos;
using Core.Entities;

namespace Core.Services.Contracts
{
    public interface IJwtServices
    {
        Task AddExpiredToken(string token);
        Task<AuthenticationResponseDto> CreateJwtToken(ApplicationUser user);
        bool IsExpiredToken(string token);
    }
}
