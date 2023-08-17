using Entities.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthService
    {
        public IEnumerable<IdentityRole> Roles { get; }
        public IEnumerable<IdentityUser> GetAllUser();
        Task<IdentityResult> CreateUser (UserDtoForCreation userDto);
        Task<UserDtoForUpdate> GetUserForUpdate(string userName);
        
        Task<IdentityUser> GetOneUser (string userName);

        Task Update(UserDtoForUpdate userDto);

        Task<IdentityResult> ResetPassword(ResetPasswordDto model);
        Task<IdentityResult> DeleteOneUser(string userName);
    }
}