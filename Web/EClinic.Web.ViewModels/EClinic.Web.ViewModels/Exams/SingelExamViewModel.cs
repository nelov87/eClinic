using EClinic.Data.Models;
using EClinic.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EClinic.Web.ViewModels.Exams
{
    public class SingelExamViewModel : IMapFrom<Exam>
    {
        
        public string Id { get; set; }

        [DisplayName("Condition")]
        public string Condition { get; set; }

        [DisplayName("DateCreated")]
        public DateTime Date { get; set; }

        [DisplayName("Diagnose")]
        public string Diagnose { get; set; }

        [DisplayName("Prescription")]
        public string Prescription { get; set; }

        public string DoctorId { get; set; }

        [DisplayName("Doctor Name")]
        public string DoctorName { get; set; }
    }
}
