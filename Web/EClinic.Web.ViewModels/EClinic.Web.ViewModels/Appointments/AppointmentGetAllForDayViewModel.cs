using EClinic.Data.Models;
using EClinic.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace EClinic.Web.ViewModels.Appointments
{
    public class AppointmentGetAllForDayViewModel : IMapFrom<Appointment>
    {
        public DateTime AppointmentDateTime { get; set; }
    }
}
