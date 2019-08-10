using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Common;
using EClinic.Services.Administration;
using EClinic.Services.Exams;
using EClinic.Web.InputModels.Patient;
using EClinic.Web.ViewModels.Administration;
using EClinic.Web.ViewModels.Exams;
using EClinic.Web.ViewModels.Patient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EClinic.Web.Areas.Patient.Controllers
{
    public class PatientController : PatientBaseController
    {
        private readonly IUsersService usersService;
        private readonly IExamService examService;
        private readonly IHostingEnvironment hostingEnvironment;

        public PatientController(IUsersService usersService, IExamService examService, IHostingEnvironment hostingEnvironment)
        {
            this.usersService = usersService;
            this.examService = examService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var user = new EditUserViewModel();
            try
            {
                user = await this.usersService.GetUser(this.User.Identity.Name);
            }
            catch (Exception e)
            {
                return this.Redirect("GetAllUsers");
            }

            ICollection<SingelExamViewModel> exams = new List<SingelExamViewModel>();

            try
            {
                exams = await this.examService.GetAllExamForPatient(this.User.Identity.Name);
            }
            catch (NullReferenceException e)
            {
                return this.Redirect("GetAllUsers");
            }

            user.Exams = exams;

            return this.View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditPatient()
        {
            //if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            //{
            //    return this.Redirect("GetAllUsers");
            //}

            EditPatientViewModel user = new EditPatientViewModel();

            try
            {
                user = await this.usersService.GetPatient(this.User.Identity.Name);
            }
            catch (Exception e)
            {
                return this.Redirect("GetAllUsers");
            }

            var newModel = new EditPatientInputModel()
            {
                Address = user.Address,
                Age = user.Age,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email
                
            };

            


            return View(newModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPatient(EditPatientInputModel viewModel)
        {
            //if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            //{
            //    return this.Redirect("GetAllUsers");
            //}

            if (!this.ModelState.IsValid)
            {
                return this.Redirect("Index");
            }

            string fileName = null;
            string fullPath = null;

            if (viewModel.Image != null)
            {

                fileName = Guid.NewGuid().ToString() + "_" + viewModel.Image.FileName;
                fullPath = Path.Combine("/img/", fileName);
                viewModel.Image.CopyTo(new FileStream(hostingEnvironment.WebRootPath + fullPath, FileMode.Create));
            }

            var newModel = new EditPatientViewModel()
            {
                Address = viewModel.Address,
                Age = viewModel.Age,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                MiddleName = viewModel.MiddleName,
                ImageUrl = fullPath == null? await this.usersService.GetUserProfilePicture(this.User.Identity.Name):fullPath,
                Email = viewModel.Email
            };



            try
            {
                await this.usersService.EditPatient(newModel);
            }
            catch (Exception e)
            {
                return this.Redirect("GetAllUsers");
            }


            return this.Redirect("Index");
        }
    }
}