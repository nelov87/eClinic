﻿namespace eClinic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SitePages
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimum lenght of Title is 3 charecters!")]
        [MaxLength(200, ErrorMessage = "Maximum lenght of Title is 20 charecters!")]
        public string Title { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimum lenght of Content is 5 charecters!")]
        [MaxLength(2000, ErrorMessage = "Maximum lenght of Content is 2000 charecters!")]
        public string Content { get; set; }
    }
}