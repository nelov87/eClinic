using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Services.Administration;
using EClinic.Services.FrontEnd;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EClinic.Web.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService appointmentService;
        private readonly IDoctorService doctorService;

        public AppointmentController(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            this.appointmentService = appointmentService;
            this.doctorService = doctorService;
        }


        public async Task<IActionResult> Index()
        {
            var doctors = await this.doctorService.GetAllDoctorsNames();

            return this.View(doctors);
        }

        public async Task<IActionResult> ShowMonth(string id, string previousNext)
        {
            var date = DateTime.UtcNow;
            int month = DateTime.UtcNow.Month;

            this.ViewData["Doctor"] = id;

            if (previousNext == "previous")
            {
                month = date.AddMonths(-1).Month;
            }
            else if (previousNext == "next")
            {
                month = date.AddMonths(1).Month;
            }
            
                this.ViewData["Month"] = month;

            return View();
        }

        public async Task<IActionResult> ShowDay(int day, int month, string doctor)
        {
            this.ViewData["Doctor"] = doctor;
            this.ViewData["Day"] = day;
            this.ViewData["Month"] = month;

            var dateOfApointment = new DateTime(DateTime.UtcNow.Year, month, day);

            var appointments = await this.appointmentService.GetAllAppointsmentForDoctorForDay(doctor, dateOfApointment);
            
            return this.View(appointments);
        }

        public async Task<IActionResult> CreateAppointment(string userName, string doctor, DateTime date)
        {
            //{ 01 - Jan - 01 12:00:00 AM}
            if (date == DateTime.MinValue)
            {
                return this.Redirect("ShowMonth");
            }

            await this.appointmentService.CreateAppointment(userName, doctor, date);
           
            return this.Redirect("ShowSuccesAppointment");
        }

        public async Task<IActionResult> ShowSuccesAppointment()
        {

            var appointment = await this.appointmentService.ShowSingelAppointment(this.User.Identity.Name);

            return this.View(appointment);
        }
    }
}