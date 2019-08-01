using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EClinic.Data.Models
{
    public class Prescription
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Prescription mus be les then 200 charecters!")]
        public string Medicaments { get; set; }
    }
}
