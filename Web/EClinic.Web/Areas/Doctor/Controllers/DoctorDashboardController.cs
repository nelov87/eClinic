using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Services.FrontEnd;
using Microsoft.AspNetCore.Mvc;

namespace EClinic.Web.Areas.Doctor.Controllers
{
    public class DoctorDashboardController : DoctorController
    {
        private readonly IAppointmentService appointmentService;

        public DoctorDashboardController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await this.appointmentService.GetAppointmentsForDoctorFull(this.User.Identity.Name);

            return View(appointments);
        }


    }
}