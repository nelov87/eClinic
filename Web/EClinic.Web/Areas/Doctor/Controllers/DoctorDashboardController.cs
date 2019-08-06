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

        public async Task<IActionResult> GetSingelAppointment(string id)
        {
            if (id == null)
            {
                return Redirect("~/Doctor/DoctorDashboard/Index");
            }

            var appointment = await this.appointmentService.ShowSingelAppointment(id);

            return this.View(appointment);
        }

        public async Task<IActionResult> DeleteAppointment(string id)
        {
            if (id == null)
            {
                return Redirect("~/Doctor/DoctorDashboard/Index");
            }

            await this.appointmentService.DeleteAppointment(id);

            return Redirect("~/Doctor/DoctorDashboard/Index");
        }
    }
}