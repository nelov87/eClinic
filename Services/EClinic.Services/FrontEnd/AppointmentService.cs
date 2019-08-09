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

        public async Task<ICollection<GetAllAppointmentFullProperties>> GetAll()
        {
            var appointments = this.db.Appointments.Select(x => new GetAllAppointmentFullProperties()
            {
                Id = x.Id,
                AppointmentDateTime = x.AppointmentDateTime,
                CreatedOn = x.CreatedOn,
                DoctorName = $"{this.db.Users.FirstOrDefault(d => d.Id == x.DoctorId).FirstName} {this.db.Users.FirstOrDefault(d => d.Id == x.DoctorId).LastName}",
                PatientName = $"{this.db.Users.FirstOrDefault(u => u.Id == x.PatientId).FirstName} {this.db.Users.FirstOrDefault(u => u.Id == x.PatientId).LastName}"
            })
                .ToList();

            return appointments;
        }

        
        public async Task<ICollection<AppointmentGetAllForDayViewModel>> GetAllAppointsmentDatesForDoctorForDay(string doctorUserName, DateTime date)
        {
            var doctorId = "";

            var appointments = new List<AppointmentGetAllForDayViewModel>();

            try
            {
                doctorId = this.db.Users.FirstOrDefault(x => x.UserName == doctorUserName).Id;

                appointments = this.db.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDateTime.Month == date.Month && a.AppointmentDateTime.Day == date.Day)
                .To<AppointmentGetAllForDayViewModel>().ToList();
            }
            catch(NullReferenceException e)
            {
                throw new ArgumentException("GetAllAppointsmentDatesForDoctorForDay Requires valid parameters");
            }

            return appointments;
        }

        public async Task<bool> CreateAppointment( string patientUsername, string doctorUserName, DateTime date)
        {

            var doctorId = "";
            var patientId = "";

            try
            {
                doctorId = this.db.Users.FirstOrDefault(x => x.UserName == doctorUserName).Id;
                patientId = this.db.Users.FirstOrDefault(x => x.UserName == patientUsername).Id;
            }
            catch(NullReferenceException e)
            {
                throw new ArgumentException("CreateAppointment Requires valid parameter.");
            }


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

        public async Task<GetSuccsesAppointmentViewModel> ShowLastAppointmentForUser(string userName)
        {
            Appointment appoitment = new Appointment();
            EClinicUser doctor = new EClinicUser();
            EClinicUser patient = new EClinicUser();
            try
            {
                appoitment = this.db.Appointments.Where(x => x.Patient.UserName == userName).OrderByDescending(x => x.CreatedOn).Take(1).FirstOrDefault(x => true);
                doctor = this.db.Users.FirstOrDefault(x => x.Id == appoitment.DoctorId);
                patient = this.db.Users.FirstOrDefault(x => x.Id == appoitment.PatientId);
            }
            catch(Exception e)
            {
                throw new ArgumentException("ShowLastAppointmentForUser needs valid username");
            }

            

            var appointmentToReturn = new GetSuccsesAppointmentViewModel()
            {
                AppointmentDadeTime = appoitment.AppointmentDateTime,
                CreatedOn = appoitment.CreatedOn,
                DoctorName = $"{doctor.FirstName} {doctor.LastName}",
                Patient = $"{patient.FirstName} {patient.LastName}",
            };

            return appointmentToReturn;
        }

        public async Task<ICollection<DoctorGetAllAppointmentsFullViewModel>> GetAppointmentsForDoctorFull(string doctorUserName)
        {
            ICollection<DoctorGetAllAppointmentsFullViewModel> appointments = new List<DoctorGetAllAppointmentsFullViewModel>();

            string doctorId = this.db.Users.FirstOrDefault(u => u.UserName == doctorUserName).Id;

            appointments = this.db.Appointments.Where(a => a.DoctorId == doctorId).Select(x => new DoctorGetAllAppointmentsFullViewModel()
            {
                Id = x.Id,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                AppointmentDateTime = x.AppointmentDateTime,
                CreatedOn = x.CreatedOn,
                DoctorName = $"{this.db.Users.FirstOrDefault(d => d.Id == x.DoctorId).FirstName} {this.db.Users.FirstOrDefault(d => d.Id == x.DoctorId).LastName}",
                PatientName = $"{this.db.Users.FirstOrDefault(d => d.Id == x.PatientId).FirstName} {this.db.Users.FirstOrDefault(d => d.Id == x.PatientId).LastName}"
            }).ToList();
            return appointments;
            
        }

        public async Task<DoctorGetAllAppointmentsFullViewModel> ShowSingelAppointment(string appointmentId)
        {
            var appointmentDb = this.db.Appointments
                .FirstOrDefault(a => a.Id == appointmentId);

            var appointment = new DoctorGetAllAppointmentsFullViewModel()
            {
                Id = appointmentDb.Id,
                DoctorId = appointmentDb.DoctorId,
                PatientId = appointmentDb.PatientId,
                AppointmentDateTime = appointmentDb.AppointmentDateTime,
                CreatedOn = appointmentDb.CreatedOn,
                DoctorName = $"{this.db.Users.FirstOrDefault(d => d.Id == appointmentDb.DoctorId).FirstName} {this.db.Users.FirstOrDefault(d => d.Id == appointmentDb.DoctorId).LastName}",
                PatientName = $"{this.db.Users.FirstOrDefault(d => d.Id == appointmentDb.PatientId).FirstName} {this.db.Users.FirstOrDefault(d => d.Id == appointmentDb.PatientId).LastName}"
            };

            return appointment;
        }

        public async Task<bool> DeleteAppointment(string appointmentId)
        {
            var appointment = this.db.Appointments.FirstOrDefault(a => a.Id == appointmentId);

            this.db.Appointments.Remove(appointment);

            int result = this.db.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}
