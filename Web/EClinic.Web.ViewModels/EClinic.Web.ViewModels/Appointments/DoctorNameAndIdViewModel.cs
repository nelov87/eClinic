using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EClinic.Web.ViewModels.Appointments
{
    public class DoctorNameAndIdViewModel
    {
        public string Id { get; set; }

        [DisplayName("Doctor")]
        public string Name { get; set; }
    }
}
