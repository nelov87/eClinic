using EClinic.Data.Models;
using EClinic.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EClinic.Web.ViewModels.Site
{
    public class PageViewModel : IMapFrom<SitePages>
    {
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
