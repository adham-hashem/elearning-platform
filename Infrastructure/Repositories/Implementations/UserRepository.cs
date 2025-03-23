//using Core.Dtos;
//using Core.DTOs;
//using Core.RepositoriesContracts;
//using Infrastructure.DB;
//using Infrastructure.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Repositories.Implementations
//{
//    public class UserRepository : Repository<AspNetUser>, IUserRepository
//    {
//        private readonly UserManager<AspNetUser> _userManager;

//        public UserRepository(ElearningDbContext context, UserManager<AspNetUser> userManager)
//            : base(context)
//        {
//            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
//        }

//        public async Task<UserDto> GetUserWithRolesAsync(Guid id)
//        {
//            var user = await _context.AspNetUsers
//                .Include(u => u.Level)
//                .FirstOrDefaultAsync(u => u.Id == id);

//            if (user == null) return null;

//            var roles = await _userManager.GetRolesAsync(user);

//            return new UserDto
//            {
//                Id = user.Id,
//                NationalId = user.NationalId,
//                FullName = user.FullName,
//                BirthDate = user.BirthDate,
//                PhoneNumber = user.PhoneNumber,
//                Address = user.Address,
//                Email = user.Email,
//                LevelId = user.LevelId,
//                LevelName = user.Level?.LevelName,
//                Roles = roles.ToList()
//            };
//        }

//        public async Task<PagedResponse<UserDto>> GetUsersWithRolesPagedAsync(int pageNumber, int pageSize, string role = null)
//        {
//            var query = _context.Users
//                .Include(u => u.Level)
//                .AsQueryable();

//            if (!string.IsNullOrEmpty(role))
//            {
//                var usersInRole = await _userManager.GetUsersInRoleAsync(role);
//                query = query.Where(u => usersInRole.Any(r => r.Id == u.Id));
//            }

//            var totalRecords = await query.CountAsync();
//            var users = await query
//                .Skip((pageNumber - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();

//            var userDtos = new List<UserDto>();
//            foreach (var user in users)
//            {
//                var roles = await _userManager.GetRolesAsync(user);
//                userDtos.Add(new UserDto
//                {
//                    Id = user.Id,
//                    NationalId = user.NationalId,
//                    FullName = user.FullName,
//                    BirthDate = user.BirthDate,
//                    PhoneNumber = user.PhoneNumber,
//                    Address = user.Address,
//                    Email = user.Email,
//                    LevelId = user.LevelId,
//                    LevelName = user.Level?.LevelName,
//                    Roles = roles.ToList()
//                });
//            }

//            return new PagedResponse<UserDto>
//            {
//                PageNumber = pageNumber,
//                PageSize = pageSize,
//                TotalRecords = totalRecords,
//                Data = userDtos
//            };
//        }

//        public async Task<bool> AssignRoleAsync(Guid userId, string role)
//        {
//            var user = await _userManager.FindByIdAsync(userId.ToString());
//            if (user == null) return false;

//            var result = await _userManager.AddToRoleAsync(user, role);
//            return result.Succeeded;
//        }

//        public async Task<bool> RemoveRoleAsync(Guid userId, string role)
//        {
//            var user = await _userManager.FindByIdAsync(userId.ToString());
//            if (user == null) return false;

//            var result = await _userManager.RemoveFromRoleAsync(user, role);
//            return result.Succeeded;
//        }
//    }
//}
