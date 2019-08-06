using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EClinic.Data;
using EClinic.Data.Models;
using EClinic.Web.InputModels.Exams;
using EClinic.Web.ViewModels.Exams;

namespace EClinic.Services.Exams
{
    public class ExamService : IExamService
    {
        private readonly EClinicDbContext db;

        public ExamService(EClinicDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> CreateExam(CreateExamInputModel examInputModel)
        {
            
            var examdb = new Exam()
            {
                Condition = examInputModel.Condition,
                Date = DateTime.UtcNow,
                Diagnose = examInputModel.Diagnose,
                DoctorId = this.db.Users.FirstOrDefault(d => d.UserName == examInputModel.DoctorUserName).Id,
                PatientId = this.db.Users.FirstOrDefault(p => p.UserName == examInputModel.PatientUserName).Id,
                Prescription = examInputModel.Prescription
            };

            this.db.Exams.Add(examdb);

            int result = this.db.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public Task<bool> DeleteExam(string examId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<SingelExamViewModel>> GetAllExamForPatient(string patientUserName)
        {
            var patientId = this.db.Users.FirstOrDefault(x => x.UserName == patientUserName).Id;

            var exams = this.db.Exams
                .Where(e => e.PatientId == patientId)
                .Select(e => new SingelExamViewModel()
                {
                    Condition = e.Condition,
                    Date = e.Date,
                    Diagnose = e.Diagnose,
                    DoctorId = e.DoctorId,
                    DoctorName = "",
                    Id = e.Id,
                    Prescription = e.Prescription
                })
                .ToList();

            return exams;
        }

        public Task<ICollection<SingelExamViewModel>> GetAllExamsForDoctor(string doctorUsername)
        {
            throw new NotImplementedException();
        }

        public Task<SingelExamViewModel> GetSingelExam(string examId)
        {
            throw new NotImplementedException();
        }
    }
}
