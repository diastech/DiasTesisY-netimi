using DIAS.Application.Exceptions;
using DIAS.Application.Wrappers;
using DIAS.Infrastructure.Identity.Interfaces;
using DIAS.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIAS.Infrastructure.Identity.Services
{
    public class ApplicationUserRoleService : IApplicationUserRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ApplicationUserRoleService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response<List<IdentityRole>>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return new Response<List<IdentityRole>>(roles, "Success");
        }

        public async Task<Response<List<IdentityRole>>> GetUserRolesAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new ApiException($"No Accounts Registered with {id}.");

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Count == 0)
                throw new ApiException($"No User Role Registered with {id}.");

            List<IdentityRole> userR = new List<IdentityRole>();
            foreach (var item in userRoles.ToList())
            {
                var r = await _roleManager.FindByNameAsync(item);
                userR.Add(r);
            }
            if (userR.Count > 0)
                return new Response<List<IdentityRole>>(userR, "Success");
            else
                throw new ApiException("Something went wrong. :)");
        }

        public async Task<Response<IdentityRole>> CreateAsync(IdentityRole model)
        {
            IdentityRole role = new IdentityRole();
            role.Name = model.Name;
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return new Response<IdentityRole>(role, "Success");
            else
                throw new ApiException("Something went wrong. :)");
        }

        public async Task<Response<IdentityRole>> UpdateAsync(IdentityRole model)
        {
            var result = await _roleManager.UpdateAsync(model);
            if (result.Succeeded)
                return new Response<IdentityRole>(model, "Success");
            else
                throw new ApiException("Something went wrong. :)");
        }

        public async Task<Response<string>> DeleteAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                throw new ApiException($"No Role found with {id}.");

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return new Response<string>(id, "Deleted");
            else
                throw new ApiException("Something went wrong. :)");
        }

        public async Task<Response<string>> AddUserRolesAsync(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new ApiException($"No User found with {id}.");

            var result = await _userManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
                return new Response<string>(role, "Added");
            else
                throw new ApiException("Something went wrong. :)");
        }

        public async Task<Response<string>> DeleteUserRolesAsync(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new ApiException($"No User found with {id}.");

            var result = await _userManager.RemoveFromRoleAsync(user, role);
            if (result.Succeeded)
                return new Response<string>(role, "DeletedUserRole");
            else
                throw new ApiException("Something went wrong. :)");
        }
    }

}
