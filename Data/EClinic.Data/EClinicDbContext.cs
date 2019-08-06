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

        public DbSet<Setting> Settings { get; set; }

        public DbSet<SitePages> SitePages { get; set; }

        public DbSet<Appointment> Appointments { get; set; }


        public EClinicDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Appointment>()
                .HasOne(x => x.Patient)
                .WithMany(x => x.Appointment)
                .HasForeignKey(x => x.PatientId);

            builder.Entity<Exam>()
                .HasOne(u => u.Patient)
                .WithMany(e => e.Exams)
                .HasForeignKey(u => u.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
