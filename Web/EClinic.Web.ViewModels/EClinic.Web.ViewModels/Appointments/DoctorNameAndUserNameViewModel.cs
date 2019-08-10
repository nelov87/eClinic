using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EClinic.Web.ViewModels.Appointments
{
    public class DoctorNameAndUserNameViewModel
    {
        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Doctor")]
        public string Name { get; set; }

        [DisplayName("Image")]
        public string ImageUrl { get; set; }
    }
}
