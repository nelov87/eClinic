using EClinic.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using EClinic.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EClinic.Web.ViewModels.Appointments
{
    public class AppointmentViewModel : IMapFrom<Appointment>
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
