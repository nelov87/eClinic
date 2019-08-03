using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EClinic.Common;
using EClinic.Data;
using EClinic.Data.Models;
using EClinic.Services.Mapping;
using EClinic.Web.ViewModels.Administration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace EClinic.Services.Administration
{
    public class UsersService : IUsersService
    {
        private readonly EClinicDbContext db;
        private readonly UserManager<EClinicUser> userManager;

        public UsersService(EClinicDbContext db, UserManager<EClinicUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var users = this.db.Users.Where(x => x.IsDeleted == false).To<UserViewModel>().ToList();

            return users;
        }

        public async Task<EditUserViewModel> GetUser(string email)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Email == email && x.IsDeleted == false);

            var roles = await userManager.GetRolesAsync(user);

            var userToReturnn = new EditUserViewModel()
            {
                Address = user.Address,
                Age = user.Age,
                CreatedOn = user.CreatedOn,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                UserRoles = roles,
                Email = user.Email
            };

            return userToReturnn;
        }
    }
}
