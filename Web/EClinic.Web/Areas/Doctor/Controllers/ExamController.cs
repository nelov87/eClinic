using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Services.Exams;
using EClinic.Web.InputModels.Exams;
using EClinic.Web.ViewModels.Exams;
using Microsoft.AspNetCore.Mvc;

namespace EClinic.Web.Areas.Doctor.Controllers
{
    public class ExamController : DoctorController
    {
        private readonly IExamService examService;

        public ExamController(IExamService examService)
        {
            this.examService = examService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateExam(string email)
        {
            this.ViewData["PatientUserName"] = email;
            //this.ViewData["ExamToReturn"] = new CreateExamInputModel();

            return View(new CreateExamInputModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam(CreateExamInputModel examInputModel)
        {
            
            if (!this.ModelState.IsValid)
            {
                return this.View(examInputModel);
            }

            try
            {
                await this.examService.CreateExam(examInputModel);
            }
            catch (NullReferenceException e)
            {
                return this.View(examInputModel);
            }

            return Redirect("~/Doctor/Users/GetAllUsers");
        }

        public async Task<IActionResult> DeleteExam(string examId)
        {
            try
            {
                await this.examService.DeleteExam(examId);
            }
            catch (NullReferenceException e)
            {
                return this.Redirect("~/Doctor/Users/GetAllUsers");
            }

            return this.Redirect("~/Doctor/Users/GetAllUsers");
        }

        [HttpGet]
        public async Task<IActionResult> EditExam(string examId)
        {
            var exam = new SingelExamViewModel();
            try
            {
                exam = await this.examService.GetSingelExam(examId);
            }
            catch (Exception e)
            {
                return this.Redirect("~/Doctor/Users/GetAllUsers");
            }

            this.ViewData["Exam"] = exam;

            var emptyExam = new ExamEditInputModel();

            emptyExam.Condition = exam.Condition;
            emptyExam.Diagnose = exam.Diagnose;
            emptyExam.Prescription = exam.Prescription;
            emptyExam.Id = exam.Id;

            return this.View(emptyExam);
        }

        [HttpPost]
        public async Task<IActionResult> EditExam(ExamEditInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("~/Doctor/Users/GetAllUsers");
            }

            try
            {
                await this.examService.EditExam(model);
            }
            catch (Exception e)
            {
                return this.Redirect("~/Doctor/Users/GetAllUsers");
            }

            return this.Redirect("~/Doctor/Users/GetAllUsers");
        }
    }
}