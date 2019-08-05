using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EClinic.Data;
using EClinic.Web.ViewModels.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EClinic.Data.Models;
using EClinic.Common;

namespace EClinic.Services.Administration
{
    public class DoctorService : IDoctorService
    {
        private readonly EClinicDbContext db;
        private readonly UserManager<EClinicUser> userManager;

        public DoctorService(EClinicDbContext db, UserManager<EClinicUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<ICollection<DoctorNameAndIdViewModel>> GetAllDoctorsNames()
        {
            var doctorsDb = await this.userManager.GetUsersInRoleAsync(GlobalConstants.DoctorRoleName);

            var doctors = doctorsDb.Select(d => new DoctorNameAndIdViewModel()
            {
                Id = d.Id,
                Name = $"{d.FirstName} {d.LastName}"
            }).ToList();

            return doctors;
        }

        
    }
}
