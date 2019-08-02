using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EClinic.Data.Models
{
    public class SiteSlides
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Title { get; set; }


        public string Description { get; set; }

        [Required]
        public string ImageURL { get; set; }
    }
}
