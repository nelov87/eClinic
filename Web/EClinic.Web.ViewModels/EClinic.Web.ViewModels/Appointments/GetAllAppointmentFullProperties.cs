using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EClinic.Web.ViewModels.Appointments
{
    public class GetAllAppointmentFullProperties
    {

        public string Id { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Scheduled for")]
        public DateTime AppointmentDateTime { get; set; }

        
        public string PatientId { get; set; }

        [DisplayName("Patient Name")]
        public string PatientName { get; set; }

        
        public string DoctorId { get; set; }

        [DisplayName("Doctor Name")]
        public string DoctorName { get; set; }

    }
}
