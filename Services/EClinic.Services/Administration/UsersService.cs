using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EClinic.Common;
using EClinic.Data;
using EClinic.Data.Models;
using EClinic.Services.Exams;
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
        private readonly IExamService examService;

        public UsersService(EClinicDbContext db, UserManager<EClinicUser> userManager, IExamService examService)
        {
            this.db = db;
            this.userManager = userManager;
            this.examService = examService;
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var users = this.db.Users.Where(x => x.IsDeleted == false).To<UserViewModel>().ToList();

            return users;
        }

        public async Task<EditUserViewModel> GetUser(string email)
        {
            var user = new EClinicUser();

            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentException("email is Null");
            }

            try
            {
                user = this.db.Users.FirstOrDefault(x => x.Email == email && x.IsDeleted == false);
            }
            catch (Exception e)
            {
                throw new ArgumentException("email is Null");
            }


            var roles = await userManager.GetRolesAsync(user);

            var exams = await this.examService.GetAllExamForPatient(email);

            var userToReturnn = new EditUserViewModel()
            {
                Address = user.Address,
                Age = user.Age,
                CreatedOn = user.CreatedOn,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                UserRoles = roles,
                Email = user.Email,
                Exams = exams
            };

            return userToReturnn;
        }

        public async Task<List<string>> GetAllRoles()
        {
            var roles = this.db.Roles.Select(x => x.Name).ToList(); ;

            return roles;
        }

        public async Task<bool> EditUser(EditUserViewModel viewModel)
        {
            EClinicUser user = new EClinicUser();

            try
            {
                user = this.db.Users.First(x => x.Email == viewModel.Email);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Edit user model is mising Email");
            }

            if (user == null)
            {
                return false;
            }

            var rolesToChange = viewModel.UserRoles;
            var allRoles = this.db.Roles.Select(x => x.Name).ToList();

            foreach (var role in allRoles)
            {
                if (viewModel.UserRoles.Contains(role))
                {
                    if (!await this.userManager.IsInRoleAsync(user, role))
                    {
                        await this.userManager.AddToRoleAsync(user, role);
                    }
                }
                else
                {
                    if (user.Email != "nelov87@gmail.com" && role != "Administrator")
                    {
                        await this.userManager.RemoveFromRoleAsync(user, role);
                    }
                }
                
            }

            user.FirstName = viewModel.FirstName;
            user.MiddleName = viewModel.MiddleName;
            user.LastName = viewModel.LastName;
            user.Address = viewModel.Address;
            user.Age = viewModel.Age;
            user.ModifiedOn = DateTime.UtcNow;
            

            var changesCount =  this.db.SaveChanges();

            if (changesCount > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteUser(string email)
        {
            var user = new EClinicUser();

            if (String.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Delete User needs valid email");
            }

            try
            {
                user = await this.db.Users.FirstOrDefaultAsync(x => x.Email == email);

            }
            catch (Exception e)
            {
                throw new ArgumentException("Delete User needs valid email");
            }


            if (user == null)
            {
                return false;
            }

            user.IsDeleted = true;
            user.DeletedOn = DateTime.UtcNow;

            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
