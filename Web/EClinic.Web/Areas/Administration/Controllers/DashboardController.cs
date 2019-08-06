using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Services.FrontEnd;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EClinic.Web.Areas.Administration.Controllers
{
    
    public class DashboardController : AdministrationController
    {
        private readonly IAppointmentService appointmentService;

        public DashboardController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("~/Identity/Account/Login");
            }

            var allAppointments = await this.appointmentService.GetAll();
            return View(allAppointments);
        }
    }
}