using EClinic.Data;
using EClinic.Data.Models;
using EClinic.Services.FrontEnd;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace EClinic.Service.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void CreateAppointment()
        {
            var options = new DbContextOptionsBuilder<EClinicDbContext>()
                    .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
                    .Options;
            var dbContext = new EClinicDbContext(options);

            var appointmentService = new AppointmentService(dbContext);

            

            var user = new EClinicUser()
            {
                Email = "nelov87@gmail.com",
                UserName = "nelov87@gmail.com",
                
            }

            await appointmentService.CreateAppointment("nelov87@gmail.com", "nelov872@gmail.com", new DateTime(2019, 08, 03, 09, 00, 00));

            var result = dbContext.Appointments.ToArray().Count();

            Assert.Equal(2, result);
        }
    }
}
