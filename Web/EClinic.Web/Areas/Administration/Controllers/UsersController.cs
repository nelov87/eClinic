using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Services.Administration;
using EClinic.Web.ViewModels.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EClinic.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }


        public async Task<IActionResult> GetAllUsers()
        {
             var users = await this.usersService.GetAllUsers();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string email)
        {
            var user = await this.usersService.GetUser(email);
            var listItems = await this.usersService.GetAllRoles();
            SelectList selectLists = new SelectList(listItems);


            this.ViewData["Roles"] = selectLists;

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel viewModel)
        {
            await this.usersService.EditUser(viewModel);
            

            return this.Redirect("GetAllUsers");
        }

        public async Task<IActionResult> DeleteUser(string email)
        {
            await this.usersService.DeleteUser(email);

            return this.Redirect("GetAllUsers");
        }
    }
}