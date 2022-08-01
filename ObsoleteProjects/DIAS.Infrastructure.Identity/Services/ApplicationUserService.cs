using DIAS.Application.Exceptions;
using DIAS.Application.Wrappers;
using DIAS.Infrastructure.Identity.Interfaces;
using DIAS.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIAS.Infrastructure.Identity.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ApplicationUserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response<List<ApplicationUser>>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return new Response<List<ApplicationUser>>(users, "Success");
        }

        public async Task<Response<ApplicationUser>> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return new Response<ApplicationUser>(user, "Success");
        }

        public async Task<Response<ApplicationUser>> CreateUserAsync(ApplicationUser user)
        {
            var userExists = await _userManager.FindByNameAsync(user.UserName);
            if (userExists != null)
                throw new ApiException($"Username already registered with {user.UserName}.");
            ApplicationUser newUser = new ApplicationUser()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed
            };
            var result = await _userManager.CreateAsync(newUser, user.PasswordHash);
            if (!result.Succeeded)
                throw new ApiException("Something went wrong. :)");
            return new Response<ApplicationUser>(user, "Success");
        }

        public async Task<Response<ApplicationUser>> UpdateUserAsync(ApplicationUser user)
        {
            ApplicationUser newUser = await _userManager.FindByIdAsync(user.Id);
            newUser.AccessFailedCount = user.AccessFailedCount;
            newUser.Email = user.Email;
            newUser.EmailConfirmed = user.EmailConfirmed;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.LockoutEnabled = user.LockoutEnabled;
            newUser.LockoutEnd = newUser.LockoutEnd;
            newUser.PhoneNumber = user.PhoneNumber;
            newUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            newUser.TwoFactorEnabled = user.TwoFactorEnabled;

            // var d =  _userManager.PasswordHasher.VerifyHashedPassword(newUser, newUser.PasswordHash, user.PasswordHash);
            if (newUser.PasswordHash != user.PasswordHash)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(newUser);
                var resetResult = await _userManager.ResetPasswordAsync(newUser, token, user.PasswordHash); ;
                if (!resetResult.Succeeded)
                    throw new ApiException("Something went wrong. :)");
            }
            var result = await _userManager.UpdateAsync(newUser);
            if (!result.Succeeded)
                throw new ApiException("Something went wrong. :)");
            return new Response<ApplicationUser>(user, "Success");
        }
    }
}

