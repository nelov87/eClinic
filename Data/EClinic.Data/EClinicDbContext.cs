using EClinic.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EClinic.Data
{
    public class EClinicDbContext : IdentityDbContext<EClinicUser, IdentityRole, string>
    {
        public DbSet<Exam> Exams { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<SitePages> SitePages { get; set; }


        public EClinicDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
