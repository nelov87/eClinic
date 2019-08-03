using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Services.Administration;
using Microsoft.AspNetCore.Mvc;

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

            return View(user);
        }
    }
}