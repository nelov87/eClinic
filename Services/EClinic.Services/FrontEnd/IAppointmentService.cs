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
        Task<ICollection<AppointmentGetAllViewModel>> GetAllAppointsmentForDoctor(string id);


        Task<ICollection<AppointmentViewModel>> GetAll();

        Task<ICollection<AppointmentGetAllForDayViewModel>> GetAllAppointsmentForDoctorForDay(string doctorid, DateTime date);

        Task<bool> CreateAppointment( string patientId, string doctorId, DateTime date);

        Task<GetSuccsesAppointmentViewModel> ShowSingelAppointment(string userName);
    }
}
