using EClinic.Data.Models;
using EClinic.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EClinic.Web.ViewModels.Appointments
{
    public class GetSuccsesAppointmentViewModel : IMapFrom<Appointment>
    {
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Scheduled for")]
        public DateTime AppointmentDadeTime { get; set; }


        public string PatientId { get; set; }

        [DisplayName("Patient")]
        public string Patient { get; set; }

        [DisplayName("Doctor Id")]
        public string DoctorId { get; set; }

        [DisplayName("Doctor Name")]
        public string DoctorName { get; set; }
    }
}
