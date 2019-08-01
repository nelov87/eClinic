using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EClinic.Data.Models
{
    public class Exam
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Condition must be atleest 5 charecters!")]
        [MaxLength(1000, ErrorMessage = "Condition is longer than 200 charecters!")]
        public string Condition { get; set; }

        [Required]
        public DateTime Date { get; set; }


        public string Diagnose { get; set; }


        public string PrescriptionId { get; set; }

        public Prescription Prescription { get; set; }

        public string DoctorId { get; set; }

        public EClinicUser Doctor { get; set; }
    }
}
