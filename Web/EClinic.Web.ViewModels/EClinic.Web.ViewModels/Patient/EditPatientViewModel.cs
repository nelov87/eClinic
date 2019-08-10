using EClinic.Services.Mapping;
using EClinic.Web.ViewModels.Exams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace EClinic.Web.ViewModels.Patient
{
    public class EditPatientViewModel 
    {
        public EditPatientViewModel()
        {
            this.Exams = new HashSet<SingelExamViewModel>();

        }


        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        // Audit info
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First name must be aleest 2 charecters!")]
        [MaxLength(20, ErrorMessage = "First name must not be more then 20 charecters!")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Middle name must be aleest 2 charecters!")]
        [MaxLength(20, ErrorMessage = "Middle name must not be more then 20 charecters!")]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name must be aleest 2 charecters!")]
        [MaxLength(20, ErrorMessage = "Last name must not be more then 20 charecters!")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Range(0, 110, ErrorMessage = "Age must be in range 0 - 110")]
        [DisplayName("Age")]
        public int Age { get; set; }

        [MinLength(2, ErrorMessage = "Address name must be aleest 2 charecters!")]
        [MaxLength(50, ErrorMessage = "Address name must not be more then 50 charecters!")]
        [DisplayName("Address")]
        public string Address { get; set; }

        public ICollection<SingelExamViewModel> Exams { get; set; }

        public string ImageUrl { get; set; }
    }
}
