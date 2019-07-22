// ReSharper disable VirtualMemberCallInConstructor
using System;
using System.ComponentModel.DataAnnotations;

namespace eClinic.Data.Models
{
    public class Prescription
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage ="Prescription mus be les then 200 charecters!")]
        public string Medicaments { get; set; }
    }
}