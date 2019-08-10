using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Common;
using EClinic.Services;
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
        private readonly IMenuService menuService;
        private readonly ISettingsService settingsService;

        public AppointmentController(IAppointmentService appointmentService, 
            IDoctorService doctorService, 
            IMenuService menuService,
            ISettingsService settingsService)
        {
            this.appointmentService = appointmentService;
            this.doctorService = doctorService;
            this.menuService = menuService;
            this.settingsService = settingsService;
        }


        public async Task<IActionResult> Index()
        {
            this.ViewData["MenuItems"] = this.menuService.GetAll();
            this.ViewData["SiteName"] = this.settingsService.GetSiteName();
            this.ViewData["PhoneNumber"] = this.settingsService.GetTelefon();
            this.ViewData["Email"] = this.settingsService.GetTelefon();

            var doctors = await this.doctorService.GetAllDoctorsNames();

            return this.View(doctors);
        }

        public async Task<IActionResult> ShowMonth(string userName)
        {
            this.ViewData["MenuItems"] = this.menuService.GetAll();
            this.ViewData["SiteName"] = this.settingsService.GetSiteName();
            this.ViewData["PhoneNumber"] = this.settingsService.GetTelefon();
            this.ViewData["Email"] = this.settingsService.GetTelefon();


            if (!await this.doctorService.IsDoctor(userName))
            {
                return this.Redirect("Index");
            }

            var date = DateTime.UtcNow;
            int monthnow = DateTime.UtcNow.Month;

            this.ViewData["Doctor"] = userName;
            this.ViewData["Month"] = monthnow;
            
            return View();
        }

        public async Task<IActionResult> ShowDay(int day, int month, string doctorUsername)
        {
            this.ViewData["MenuItems"] = this.menuService.GetAll();
            this.ViewData["SiteName"] = this.settingsService.GetSiteName();
            this.ViewData["PhoneNumber"] = this.settingsService.GetTelefon();
            this.ViewData["Email"] = this.settingsService.GetTelefon();

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
            this.ViewData["MenuItems"] = this.menuService.GetAll();
            this.ViewData["SiteName"] = this.settingsService.GetSiteName();
            this.ViewData["PhoneNumber"] = this.settingsService.GetTelefon();
            this.ViewData["Email"] = this.settingsService.GetTelefon();

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
            this.ViewData["MenuItems"] = this.menuService.GetAll();
            this.ViewData["SiteName"] = this.settingsService.GetSiteName();
            this.ViewData["PhoneNumber"] = this.settingsService.GetTelefon();
            this.ViewData["Email"] = this.settingsService.GetTelefon();

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


            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                if (id == null)
                {
                    return Redirect("~/Administration/Dashboard/Index");
                }

                await this.appointmentService.DeleteAppointment(id);

                return Redirect("~/Administration/Dashboard/Index");
            }
            else if (this.User.IsInRole(GlobalConstants.DoctorRoleName))
            {
                
                if (id == null)
                {
                    return Redirect("~/Doctor/DoctorDashboard/Index");
                }

                await this.appointmentService.DeleteAppointment(id);

                return Redirect("~/Doctor/DoctorDashboard/Index");
            }

            return this.Redirect("/");
        }

    }
}