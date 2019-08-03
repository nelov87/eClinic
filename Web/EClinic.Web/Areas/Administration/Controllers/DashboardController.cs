using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EClinic.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class DashboardController : Controller
    {
        
        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("~/Identity/Account/Login");
            }

            return View();
        }
    }
}