using EClinic.Web.ViewModels.Appointments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EClinic.Services.Administration
{
    public interface IDoctorService
    {
        Task<ICollection<DoctorNameAndUserNameViewModel>> GetAllDoctorsNames();

        Task<bool> IsDoctor(string username);
    }
}
