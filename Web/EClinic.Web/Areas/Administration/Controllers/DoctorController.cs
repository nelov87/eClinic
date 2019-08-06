using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Services.Administration;
using EClinic.Services.FrontEnd;
using Microsoft.AspNetCore.Mvc;

namespace EClinic.Web.Areas.Administration.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IAppointmentService appointmentService;
        private readonly IDoctorService doctorService;

        public DoctorController(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            this.appointmentService = appointmentService;
            this.doctorService = doctorService;
        }

        public IActionResult Index(string priviosNext)
        {
            var appointments = this.appointmentService.GetAppointmentsForDoctorFull(this.User.Identity.Name);

            return View();
        }
    }
}