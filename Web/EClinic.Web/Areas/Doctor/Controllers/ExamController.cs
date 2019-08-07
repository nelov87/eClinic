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

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam(CreateExamInputModel examInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return Redirect("CreateExam");
            }

            await this.examService.CreateExam(examInputModel);

            return Redirect("~/Doctor/Users/GetAllUsers");
        }

        public async Task<IActionResult> DeleteExam(string examId)
        {
            await this.examService.DeleteExam(examId);

            return this.Redirect("~/Doctor/Users/GetAllUsers");
        }

        [HttpGet]
        public async Task<IActionResult> EditExam(string examId)
        {
            var exam = await this.examService.GetSingelExam(examId);

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
            //if (!this.ModelState.IsValid)
            //{
            //    return this.Redirect("~/Doctor/Users/GetAllUsers");
            //}

            await this.examService.EditExam(model);

            return this.Redirect("~/Doctor/Users/GetAllUsers");
        }
    }
}