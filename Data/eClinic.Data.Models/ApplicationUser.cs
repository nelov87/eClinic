// ReSharper disable VirtualMemberCallInConstructor
namespace eClinic.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using eClinic.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Exams = new HashSet<Exam>();
            this.Prescriptions = new HashSet<Prescription>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Required]
        [MinLength(2, ErrorMessage ="First name must be aleest 2 charecters!")]
        [MaxLength(20, ErrorMessage = "First name must not be more then 20 charecters!")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Middle name must be aleest 2 charecters!")]
        [MaxLength(20, ErrorMessage = "Middle name must not be more then 20 charecters!")]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Last name must be aleest 2 charecters!")]
        [MaxLength(20, ErrorMessage = "Last name must not be more then 20 charecters!")]
        public string LastName { get; set; }

        [Required]
        [Range(0, 110, ErrorMessage ="Age must be in range 0 - 110")]
        public int Age { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Address name must be aleest 2 charecters!")]
        [MaxLength(50, ErrorMessage = "Address name must not be more then 50 charecters!")]
        public string Address { get; set; }

        public ICollection<Exam> Exams { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
