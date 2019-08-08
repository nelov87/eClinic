using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Services.Administration;
using EClinic.Services.FrontEnd;
using EClinic.Web.ViewModels.Appointments;
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

        public IActionResult ShowMonth(string userName)
        {
            var date = DateTime.UtcNow;
            int monthnow = DateTime.UtcNow.Month;

            this.ViewData["Doctor"] = userName;
            this.ViewData["Month"] = monthnow;
            
            return View();
        }

        public async Task<IActionResult> ShowDay(int day, int month, string doctorUsername)
        {
            this.ViewData["Doctor"] = doctorUsername;
            this.ViewData["Day"] = day;
            this.ViewData["Month"] = month;

            var dateOfApointment = new DateTime(DateTime.UtcNow.Year, month, day);

            ICollection<AppointmentGetAllForDayViewModel> appointments = new List<AppointmentGetAllForDayViewModel>();

            try
            {
                appointments = await this.appointmentService.GetAllAppointsmentDatesForDoctorForDay(doctorUsername, dateOfApointment);

            }
            catch(ArgumentException ae)
            {
                return this.Redirect("ShowMonth");
            }

            return this.View(appointments);
        }

        public async Task<IActionResult> CreateAppointment(string userName, string doctorUsername, DateTime date)
        {
            //{ 01 - Jan - 01 12:00:00 AM}
            if (date == DateTime.MinValue )
            {
                return this.Redirect("ShowMonth");
            }

            try
            {
                await this.appointmentService.CreateAppointment(userName, doctorUsername, date);

            }
            catch(Exception e)
            {
                return this.Redirect("ShowMonth");
            }


            return this.Redirect("ShowSuccesAppointment");
        }

        public async Task<IActionResult> ShowSuccesAppointment()
        {
            GetSuccsesAppointmentViewModel appointment = new GetSuccsesAppointmentViewModel();
            try
            {
                appointment = await this.appointmentService.ShowLastAppointmentForUser(this.User.Identity.Name);

            }
            catch (Exception e)
            {
                return this.Redirect("/");
            }

            return this.View(appointment);
        }

        
        
    }
}