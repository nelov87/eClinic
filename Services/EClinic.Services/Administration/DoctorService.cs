﻿using System;
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
using EClinic.Web.ViewModels.Administration;

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

        public async Task<ICollection<DoctorNameAndUserNameViewModel>> GetAllDoctorsNames()
        {
            var doctorsDb = await this.userManager.GetUsersInRoleAsync(GlobalConstants.DoctorRoleName);

            var doctors = doctorsDb.Select(d => new DoctorNameAndUserNameViewModel()
            {
                UserName = d.UserName,
                Name = $"{d.FirstName} {d.LastName}",
                ImageUrl = d.ImageUrl
            }).ToList();

            return doctors;
        }

        public async Task<bool> IsDoctor(string username)
        {
            var isInRoleDoctor = false;

            try
            {
                var user = await this.db.Users.FirstOrDefaultAsync(d => d.UserName == username);
                isInRoleDoctor = await this.userManager.IsInRoleAsync(user, GlobalConstants.DoctorRoleName);
            }
            catch (Exception e)
            {
                return false;
            }


            return isInRoleDoctor;
        }


    }
}
