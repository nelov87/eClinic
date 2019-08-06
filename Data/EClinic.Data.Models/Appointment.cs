using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EClinic.Data.Models
{
    public class Appointment
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }


        public DateTime AppointmentDateTime { get; set; }

        [Required]
        public string PatientId { get; set; }
        public EClinicUser Patient { get; set; }

        [Required]
        public string DoctorId { get; set; }
    }
}
