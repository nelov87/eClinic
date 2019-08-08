using AutoMapper;
using EClinic.Data;
using EClinic.Data.Models;
using EClinic.Services.FrontEnd;
using EClinic.Services.Mapping;
using EClinic.Web.ViewModels.Appointments;
using EClinic.Web.ViewModels.Site;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xunit;

namespace EClinic.Service.Tests
{
    public class AppointmentServiceTests
    {
        [Fact]
        public async void CreateAppointmentForUnexistingUserAndDoctorShouldThrowExeption()
        {
            var options = new DbContextOptionsBuilder<EClinicDbContext>()
                    .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
                    .Options;
            var dbContext = new EClinicDbContext(options);

            var appointmentService = new AppointmentService(dbContext);


            await Assert.ThrowsAsync<ArgumentException> (async () => await appointmentService.CreateAppointment(null, "nelov872@gmail.com", new DateTime(2019, 08, 03, 09, 00, 00))) ;

        }

        [Fact]
        public async void CreateAppointmentForUnexistingUserAndDoctorShouldReturnTrue()
        {
            //Arrange

            var options = new DbContextOptionsBuilder<EClinicDbContext>()
                    .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
                    .Options;
            var dbContext = new EClinicDbContext(options);

            var appointmentService = new AppointmentService(dbContext);

            //var usrManager = MockHelpers.MockUserManager<EClinicUser>();

            var mockUserStore = new Mock<IUserStore<EClinicUser>>();

            var userManager = new UserManager<EClinicUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            //userManager.UserValidators.Add(new UserValidator<EClinicUser>());
            //userManager.PasswordValidators.Add(new PasswordValidator<EClinicUser>());


            var AdminUserToAdd = new EClinicUser
            {
                Email = "nelov87@gmail.com",
                FirstName = "Ivo",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov87@gmail.com",
                NormalizedEmail = "NELOV87@GMAIL.COM",
                NormalizedUserName = "NELOV87@GMAIL.COM",
                Address = "Nqkyde Tam 35",
                Age = 25,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
            };

            var userToAdd = new EClinicUser
            {
                Email = "nelov872@gmail.com",
                FirstName = "Petyr",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov872@gmail.com",
                NormalizedEmail = "NELOV872@GMAIL.COM",
                NormalizedUserName = "NELOV872@GMAIL.COM",
                Address = "I tuk i tam",
                Age = 30,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
            };


            //await userManager.CreateAsync(AdminUserToAdd);
            //await userManager.CreateAsync(userToAdd);
            dbContext.Users.Add(AdminUserToAdd);
            dbContext.Users.Add(userToAdd);

            dbContext.SaveChanges();


            int count = dbContext.Users.Count();

            var result = await appointmentService.CreateAppointment("nelov87@gmail.com", "nelov872@gmail.com", new DateTime(2019, 08, 03, 09, 00, 00));


            Assert.True(result);
        }


        [Fact]
        public async void GetAllAppointsmentDatesForDoctorForDayShoulReturnOne()
        {
            //Arrange

            var options = new DbContextOptionsBuilder<EClinicDbContext>()
                    .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
                    .Options;
            var dbContext = new EClinicDbContext(options);

            var appointmentService = new Mock<AppointmentService>(dbContext).Object;
            AutoMapperConfig.RegisterMappings(typeof(SetingViewModel).GetTypeInfo().Assembly);
            var mockUserStore = new Mock<IUserStore<EClinicUser>>();
            
            var userManager = new UserManager<EClinicUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);
           
            var AdminUserToAdd = new EClinicUser
            {
                Email = "nelov87@gmail.com",
                FirstName = "Ivo",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov87@gmail.com",
                NormalizedEmail = "NELOV87@GMAIL.COM",
                NormalizedUserName = "NELOV87@GMAIL.COM",
                Address = "Nqkyde Tam 35",
                Age = 25,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
            };

            var userToAdd = new EClinicUser
            {
                Email = "nelov872@gmail.com",
                FirstName = "Petyr",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov872@gmail.com",
                NormalizedEmail = "NELOV872@GMAIL.COM",
                NormalizedUserName = "NELOV872@GMAIL.COM",
                Address = "I tuk i tam",
                Age = 30,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
            };

            dbContext.Users.Add(AdminUserToAdd);
            dbContext.Users.Add(userToAdd);

            dbContext.SaveChanges();

            var date = new DateTime(2019, 08, 03, 09, 00, 00);

            await appointmentService.CreateAppointment("nelov87@gmail.com", "nelov872@gmail.com", date);

            dbContext.Appointments.Where(x => true).ToList();
            
            //Act
            var result = await appointmentService.GetAllAppointsmentDatesForDoctorForDay(userToAdd.UserName, date);

            var actual = dbContext.Appointments.Where(x => x.DoctorId == dbContext.Users.FirstOrDefault(d => d.Email == "nelov872@gmail.com").Id).ToList();
            
            //Assert
            Assert.Equal(actual.Count(), result.Count());
        }

        [Fact]
        public async void GetAllAppointsmentDatesForDoctorForDayShoulThrowExeption()
        {
            //Arrange

            var options = new DbContextOptionsBuilder<EClinicDbContext>()
                    .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
                    .Options;
            var dbContext = new EClinicDbContext(options);

            var appointmentService = new Mock<AppointmentService>(dbContext).Object;
            AutoMapperConfig.RegisterMappings(typeof(SetingViewModel).GetTypeInfo().Assembly);
            var mockUserStore = new Mock<IUserStore<EClinicUser>>();

            var userManager = new UserManager<EClinicUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var AdminUserToAdd = new EClinicUser
            {
                Email = "nelov87@gmail.com",
                FirstName = "Ivo",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov87@gmail.com",
                NormalizedEmail = "NELOV87@GMAIL.COM",
                NormalizedUserName = "NELOV87@GMAIL.COM",
                Address = "Nqkyde Tam 35",
                Age = 25,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
            };

            var userToAdd = new EClinicUser
            {
                Email = "nelov872@gmail.com",
                FirstName = "Petyr",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov872@gmail.com",
                NormalizedEmail = "NELOV872@GMAIL.COM",
                NormalizedUserName = "NELOV872@GMAIL.COM",
                Address = "I tuk i tam",
                Age = 30,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
            };

            dbContext.Users.Add(AdminUserToAdd);
            dbContext.Users.Add(userToAdd);

            dbContext.SaveChanges();

            var date = new DateTime(2019, 08, 03, 09, 00, 00);

            await appointmentService.CreateAppointment("nelov87@gmail.com", "nelov872@gmail.com", date);

            dbContext.Appointments.Where(x => true).ToList();

            
            

            await Assert.ThrowsAsync<ArgumentException>(async () => await appointmentService.GetAllAppointsmentDatesForDoctorForDay("", date));
        }

        [Fact]
        public async void ShowLastAppointmentForUserShoulThrowExeption()
        {
            //Arrange

            var options = new DbContextOptionsBuilder<EClinicDbContext>()
                    .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
                    .Options;
            var dbContext = new EClinicDbContext(options);

            var appointmentService = new Mock<AppointmentService>(dbContext).Object;
            AutoMapperConfig.RegisterMappings(typeof(SetingViewModel).GetTypeInfo().Assembly);
            var mockUserStore = new Mock<IUserStore<EClinicUser>>();

            var userManager = new UserManager<EClinicUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var AdminUserToAdd = new EClinicUser
            {
                Email = "nelov87@gmail.com",
                FirstName = "Ivo",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov87@gmail.com",
                NormalizedEmail = "NELOV87@GMAIL.COM",
                NormalizedUserName = "NELOV87@GMAIL.COM",
                Address = "Nqkyde Tam 35",
                Age = 25,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
            };

            var userToAdd = new EClinicUser
            {
                Email = "nelov872@gmail.com",
                FirstName = "Petyr",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov872@gmail.com",
                NormalizedEmail = "NELOV872@GMAIL.COM",
                NormalizedUserName = "NELOV872@GMAIL.COM",
                Address = "I tuk i tam",
                Age = 30,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
            };

            dbContext.Users.Add(AdminUserToAdd);
            dbContext.Users.Add(userToAdd);

            dbContext.SaveChanges();

            var date = new DateTime(2019, 08, 03, 09, 00, 00);

            await appointmentService.CreateAppointment("nelov87@gmail.com", "nelov872@gmail.com", date);

            dbContext.Appointments.Where(x => true).ToList();


            await Assert.ThrowsAsync<ArgumentException>(async () => await appointmentService.ShowLastAppointmentForUser(""));
        }
    }
}
