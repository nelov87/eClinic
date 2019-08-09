using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Common;
using EClinic.Services.Administration;
using EClinic.Services.Exams;
using EClinic.Web.ViewModels.Administration;
using EClinic.Web.ViewModels.Exams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EClinic.Web.Areas.Doctor.Controllers
{
    
    public class UsersController : DoctorController
    {
        private readonly IUsersService usersService;
        private readonly IExamService examService;

        public UsersController(IUsersService usersService, IExamService examService)
        {
            this.usersService = usersService;
            this.examService = examService;
        }


        public async Task<IActionResult> GetAllUsers()
        {
             var users = await this.usersService.GetAllUsers();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string email)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("GetAllUsers");
            }

            EditUserViewModel user = new EditUserViewModel();

            try
            {
                user = await this.usersService.GetUser(email);
            }
            catch (Exception e)
            {
                return this.Redirect("GetAllUsers");
            }

            var listItems = await this.usersService.GetAllRoles();
            SelectList selectLists = new SelectList(listItems);


            this.ViewData["Roles"] = selectLists;

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel viewModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("GetAllUsers");
            }

            try
            {
                await this.usersService.EditUser(viewModel);
            }
            catch (Exception e)
            {
                return this.Redirect("GetAllUsers");
            }
            

            return this.Redirect("GetAllUsers");
        }

        public async Task<IActionResult> DeleteUser(string email)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("GetAllUsers");
            }

            try
            {
                await this.usersService.DeleteUser(email);
            }
            catch (Exception e)
            {
                return this.Redirect("GetAllUsers");
            }

            return this.Redirect("GetAllUsers");
        }

        public async Task<IActionResult> GetUserInfo(string email)
        {
            var user = new EditUserViewModel();
            try
            {
                user = await this.usersService.GetUser(email);
            }
            catch (Exception e)
            {
                return this.Redirect("GetAllUsers");
            }

            ICollection<SingelExamViewModel> exams = new List<SingelExamViewModel>();

            try
            {
                exams = await this.examService.GetAllExamForPatient(email);
            }
            catch (NullReferenceException e)
            {
                return this.Redirect("GetAllUsers");
            }

            user.Exams = exams;

            return this.View(user);
        }
    }
}