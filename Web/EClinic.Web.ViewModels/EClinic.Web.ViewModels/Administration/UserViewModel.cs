using EClinic.Data.Models;
using EClinic.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EClinic.Web.ViewModels.Administration
{
    public class UserViewModel : IMapFrom<EClinicUser>
    {
        public string Username { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First name must be aleest 2 charecters!")]
        [MaxLength(20, ErrorMessage = "First name must not be more then 20 charecters!")]
        public string Email { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "First name must be aleest 2 charecters!")]
        [MaxLength(20, ErrorMessage = "First name must not be more then 20 charecters!")]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Middle name must be aleest 2 charecters!")]
        [MaxLength(20, ErrorMessage = "Middle name must not be more then 20 charecters!")]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name must be aleest 2 charecters!")]
        [MaxLength(20, ErrorMessage = "Last name must not be more then 20 charecters!")]
        public string LastName { get; set; }

        [Range(0, 110, ErrorMessage = "Age must be in range 0 - 110")]
        public int Age { get; set; }

        

        
    }
}
