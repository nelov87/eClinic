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
            try
            {
                if (String.IsNullOrWhiteSpace(examInputModel.Condition) 
                    || String.IsNullOrWhiteSpace(examInputModel.Diagnose)
                    || String.IsNullOrWhiteSpace(examInputModel.Prescription))
                {
                    throw new NullReferenceException("Condition, Doagnose, Prescription should be valid values.");
                }


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
            }
            catch (NullReferenceException e)
            {
                return false;
            }

            int result = this.db.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteExam(string examId)
        {
            if (String.IsNullOrWhiteSpace(examId))
            {
                throw new NullReferenceException("DeleteExam requires id.");
            }

            try
            {
                this.db.Exams.Remove(this.db.Exams.FirstOrDefault(x => x.Id == examId));
            }
            catch (Exception e)
            {
                throw new NullReferenceException("There was an error while deleting exam" );
            }

            int result = this.db.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<ICollection<SingelExamViewModel>> GetAllExamForPatient(string patientUserName)
        {
            var patientId = "";

            try
            {
                patientId = this.db.Users.FirstOrDefault(x => x.UserName == patientUserName).Id;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("GetAllExamForPatient requires patientUserName to be not null or white space.");

            }

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

        //public Task<ICollection<SingelExamViewModel>> GetAllExamsForDoctor(string doctorUsername)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<SingelExamViewModel> GetSingelExam(string examId)
        {
            if (String.IsNullOrWhiteSpace(examId))
            {
                throw new NullReferenceException("GetSingelExam requires exam Id to be not null ot white space.");
            }

            var exam = this.db.Exams
                .To<SingelExamViewModel>()
                .FirstOrDefault(e => e.Id == examId);
                

            return exam;
        }

        public async Task<bool> EditExam(ExamEditInputModel inputModel)
        {
            var examDb = new Exam();

            if (String.IsNullOrWhiteSpace(inputModel.Id))
            {
                throw new NullReferenceException("Id must not be null or whitespace");
            }

            try
            {
                examDb = this.db.Exams.FirstOrDefault(x => x.Id == inputModel.Id);
            }
            catch (Exception e)
            {
                throw new NullReferenceException("There was an error in EditExam");
            }

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
