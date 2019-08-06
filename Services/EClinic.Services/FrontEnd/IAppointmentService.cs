using EClinic.Web.Appointments.InputModels;
using EClinic.Web.ViewModels.Appointments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EClinic.Services.FrontEnd
{
    public interface IAppointmentService
    {
        //Task<ICollection<AppointmentGetAllViewModel>> GetAllAppointsmentForDoctorDates(string id);


        Task<ICollection<GetAllAppointmentFullProperties>> GetAll();

        Task<ICollection<AppointmentGetAllForDayViewModel>> GetAllAppointsmentDatesForDoctorForDay(string doctorid, DateTime date);

        Task<bool> CreateAppointment( string patientId, string doctorId, DateTime date);

        Task<GetSuccsesAppointmentViewModel> ShowLastAppointmentForUser(string userName);

        Task<ICollection<DoctorGetAllAppointmentsFullViewModel>> GetAppointmentsForDoctorFull(string doctorUserName);

        Task<DoctorGetAllAppointmentsFullViewModel> ShowSingelAppointment(string appointmentId);

        Task<bool> DeleteAppointment(string appointmentId);

    }
}
