using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EClinic.Services.Exams;
using EClinic.Web.InputModels.Exams;
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
        public async Task<IActionResult> CreateExam()
        {
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

            return Redirect("ShowAllExams");
        }
    }
}