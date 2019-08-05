using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EClinic.Data;
using EClinic.Data.Models;
using EClinic.Services.Mapping;
using EClinic.Web.Appointments.InputModels;
using EClinic.Web.ViewModels.Appointments;

namespace EClinic.Services.FrontEnd
{
    public class AppointmentService : IAppointmentService
    {
        private readonly EClinicDbContext db;

        public AppointmentService(EClinicDbContext db)
        {
            this.db = db;
        }

        public async Task<ICollection<AppointmentViewModel>> GetAll()
        {
            var appointments = this.db.Appointments.Select(x => new AppointmentViewModel()
            {
                AppointmentDadeTime = x.AppointmentDateTime,
                CreatedOn = x.CreatedOn,
                DoctorName = $"{this.db.Users.FirstOrDefault(d => d.Id == x.Id).FirstName} {this.db.Users.FirstOrDefault(d => d.Id == x.Id).LastName}",
                Patient = $"{this.db.Users.FirstOrDefault(u => u.Id == x.Id).FirstName} {this.db.Users.FirstOrDefault(u => u.Id == x.Id).LastName}"
            })
                .ToList();

            return appointments;
        }

        public async Task<ICollection<AppointmentGetAllViewModel>> GetAllAppointsmentForDoctor(string id)
        {
            var appointments = this.db.Appointments
                .Where(a => a.DoctorId == id)
                .To<AppointmentGetAllViewModel>().ToList();

            return appointments;
        }

        public async Task<ICollection<AppointmentGetAllForDayViewModel>> GetAllAppointsmentForDoctorForDay(string doctorid, DateTime date)
        {
            var appointments = this.db.Appointments
                .Where(a => a.DoctorId == doctorid)
                .To<AppointmentGetAllForDayViewModel>().ToList();

            //foreach (var item in appointments)
            //{
            //    var a = item.AppointmentDateTime.Month;

            //    var b = item.AppointmentDateTime.Day;
            //}

            return appointments;
        }

        public async Task<bool> CreateAppointment( string patientUsername, string doctorId, DateTime date)
        {
            var patientId = this.db.Users.FirstOrDefault(x => x.UserName == patientUsername).Id;

            var appointmentToAdd = new Appointment()
            {
                AppointmentDateTime = date,
                PatientId = patientId,
                DoctorId = doctorId,
                CreatedOn = DateTime.UtcNow
            };

            this.db.Appointments.Add(appointmentToAdd);

            var result = this.db.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<GetSuccsesAppointmentViewModel> ShowSingelAppointment(string userName)
        {

            var appoitment = this.db.Appointments.Where(x => x.Patient.UserName == userName).OrderByDescending(x => x.CreatedOn).Take(1).FirstOrDefault(x => true);
            var doctor = this.db.Users.FirstOrDefault(x => x.Id == appoitment.DoctorId);
            var patient = this.db.Users.FirstOrDefault(x => x.Id == appoitment.PatientId);

            var appointmentToReturn = new GetSuccsesAppointmentViewModel()
            {
                AppointmentDadeTime = appoitment.AppointmentDateTime,
                CreatedOn = appoitment.CreatedOn,
                DoctorName = $"{doctor.FirstName} {doctor.LastName}",
                Patient = $"{patient.FirstName} {patient.LastName}",
            };

            return appointmentToReturn;
        }
    }
}
