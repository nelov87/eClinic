using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EClinic.Data;
using EClinic.Data.Models;
using EClinic.Web.InputModels.Exams;
using EClinic.Web.ViewModels.Exams;
using EClinic.Services.Mapping;

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

        public async Task<bool> DeleteExam(string examId)
        {
            
            this.db.Exams.Remove(this.db.Exams.FirstOrDefault(x => x.Id == examId));

            int result = this.db.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
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
                    DoctorName = $"{e.Doctor.FirstName} {e.Doctor.LastName}",
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

        public async Task<SingelExamViewModel> GetSingelExam(string examId)
        {
            var exam = this.db.Exams
                .To<SingelExamViewModel>()
                .FirstOrDefault(e => e.Id == examId);
                

            return exam;
        }

        public async Task<bool> EditExam(ExamEditInputModel inputModel)
        {
            var examDb = this.db.Exams.FirstOrDefault(x => x.Id == inputModel.Id);

            examDb.Condition = inputModel.Condition;
            examDb.Diagnose = inputModel.Diagnose;
            examDb.Prescription = inputModel.Prescription;

            int result = this.db.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}
