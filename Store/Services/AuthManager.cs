using AutoMapper;
using Entities.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services
{
    public class AuthManager : IAuthService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;

        public AuthManager(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IEnumerable<IdentityRole> Roles => roleManager.Roles;

        public async Task<IdentityResult> CreateUser(UserDtoForCreation userDto)
        {
            var user = mapper.Map<IdentityUser>(userDto);
            var result = await userManager.CreateAsync(user , userDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User could not be created.");
            }
            if (userDto.Roles.Count>0)
            {
                var roleResult = await userManager.AddToRolesAsync(user,userDto.Roles);
                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with roles.");  
                
            }


            return result;
        }

        public IEnumerable<IdentityUser> GetAllUser()
        {
            return userManager.Users.ToList();
        }

        public async Task<IdentityUser> GetOneUser(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<UserDtoForUpdate> GetUserForUpdate(string userName)
        {
            var user = await GetOneUser(userName);
            if (user is not null)
            {
                var userDto = mapper.Map<UserDtoForUpdate>(user);
                userDto.Roles=new HashSet<string>(Roles.Select(r=>r.Name).ToList());
                userDto.UserRoles= new HashSet<string>(await userManager.GetRolesAsync(user));
                return userDto;
            }
            throw new Exception("An error occured.");
        }

        public async Task Update(UserDtoForUpdate userDto)
        {
            var user = await GetOneUser(userDto.UserName);
            user.PhoneNumber=userDto.PhoneNumber;
            user.Email=userDto.Email;

            if (user is not null)
            {
                var result = await userManager.UpdateAsync(user);
            if (userDto.Roles.Count>0)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var r1= await userManager.RemoveFromRolesAsync(user,userRoles);
                var r2 = await userManager.AddToRolesAsync(user , userDto.Roles);
            }
            }
        }
    }
}