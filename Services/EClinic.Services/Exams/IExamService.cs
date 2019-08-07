using EClinic.Web.InputModels.Exams;
using EClinic.Web.ViewModels.Exams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EClinic.Services.Exams
{
    public interface IExamService
    {
        Task<SingelExamViewModel> GetSingelExam(string examId);

        Task<bool> CreateExam(CreateExamInputModel examInputModel);

        Task<ICollection<SingelExamViewModel>> GetAllExamsForDoctor(string doctorUsername);

        Task<ICollection<SingelExamViewModel>> GetAllExamForPatient(string patientUserName);

        Task<bool> DeleteExam(string examId);

        Task<bool> EditExam(ExamEditInputModel inputModel);

    }
}
