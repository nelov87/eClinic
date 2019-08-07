using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EClinic.Web.InputModels.Exams
{
    public class ExamEditInputModel 
    {
        [Required]
        [DisplayName("Id")]
        public string Id { get; set; }

        [Required]
        [DisplayName("Condition")]
        [MinLength(5, ErrorMessage = "Condition must be atleest 5 charecters!")]
        [MaxLength(1000, ErrorMessage = "Condition is longer than 200 charecters!")]
        public string Condition { get; set; }

        [DisplayName("DateCreated")]
        public DateTime Date { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Condition must be atleest 5 charecters!")]
        [MaxLength(1000, ErrorMessage = "Condition is longer than 200 charecters!")]
        [DisplayName("Diagnose")]
        public string Diagnose { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Condition must be atleest 5 charecters!")]
        [MaxLength(1000, ErrorMessage = "Condition is longer than 200 charecters!")]
        [DisplayName("Prescription")]
        public string Prescription { get; set; }

        
    }
}
