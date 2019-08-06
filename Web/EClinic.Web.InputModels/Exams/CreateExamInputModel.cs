using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EClinic.Web.InputModels.Exams
{
    public class CreateExamInputModel
    {
        
        [Required]
        [MinLength(5, ErrorMessage = "Condition must be atleest 5 charecters!")]
        [MaxLength(1000, ErrorMessage = "Condition is longer than 200 charecters!")]
        [DisplayName("Condition")]
        public string Condition { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Condition must be atleest 5 charecters!")]
        [MaxLength(1000, ErrorMessage = "Condition is longer than 200 charecters!")]
        [DisplayName("Diagnose")]
        public string Diagnose { get; set; }

        [Required]
        [DisplayName("Prescription")]
        public string Prescription { get; set; }


        [Required]
        [DisplayName("Doctor Name")]
        public string DoctorUserName { get; set; }
    }
}
