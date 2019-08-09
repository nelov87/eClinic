using EClinic.Data;
using EClinic.Data.Models;
using EClinic.Services.Administration;
using EClinic.Services.Exams;
using EClinic.Services.Mapping;
using EClinic.Web.ViewModels.Administration;
using EClinic.Web.ViewModels.Site;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Identity.Test;
using EClinic.Services.FrontEnd;
using EClinic.Web.InputModels.Exams;

namespace EClinic.Service.Tests
{
    public class UsersServiceTests
    {
        //Arrange

        [Fact]
        public async void GetUserWhitEmptyModelShoulThrowExeption()
        {
            var options = new DbContextOptionsBuilder<EClinicDbContext>()
               .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
               .Options;
            var dbContext = new EClinicDbContext(options);

            //var mockUserStore = new Mock<IUserStore<EClinicUser>>();
            //var mockRoleManager = new Mock<IUserRoleStore<EClinicUser>>();

            //var userManager = new UserManager<EClinicUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var userManager = MockHelpers.MockUserManager<EClinicUser>().Object;

            var examService = new Mock<ExamService>(dbContext).Object;
            var appointmentService = new Mock<AppointmentService>(dbContext).Object;
            var usersService = new Mock<UsersService>(dbContext, userManager, examService).Object;

            AutoMapperConfig.RegisterMappings(typeof(SetingViewModel).GetTypeInfo().Assembly);




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
            var exam = new CreateExamInputModel()
            {
                Condition = "good",
                DoctorUserName = "nelov87@gmail.com",
                Diagnose = "adss",
                PatientUserName = "nelov872@gmail.com",
                Prescription = "sdfsdd"
            };

            await examService.CreateExam(exam);

            dbContext.Appointments.Where(x => true).ToList();


            await Assert.ThrowsAsync<ArgumentException>(async () => await usersService.GetUser(""));
        }

        [Fact]
        public async void EditUserShoulThrowExeption()
        {
            var options = new DbContextOptionsBuilder<EClinicDbContext>()
                .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
                .Options;
            var dbContext = new EClinicDbContext(options);

            //var mockUserStore = new Mock<IUserStore<EClinicUser>>();
            //var mockRoleManager = new Mock<IUserRoleStore<EClinicUser>>();

            //var userManager = new UserManager<EClinicUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var userManager = MockHelpers.MockUserManager<EClinicUser>().Object;

            var examService = new Mock<ExamService>(dbContext).Object;
            var appointmentService = new Mock<AppointmentService>(dbContext).Object;
            var usersService = new Mock<UsersService>(dbContext, userManager, examService).Object;

            AutoMapperConfig.RegisterMappings(typeof(SetingViewModel).GetTypeInfo().Assembly);




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
            var exam = new CreateExamInputModel()
            {
                Condition = "good",
                DoctorUserName = "nelov87@gmail.com",
                Diagnose = "adss",
                PatientUserName = "nelov872@gmail.com",
                Prescription = "sdfsdd"
            };

            await examService.CreateExam(exam);
            

            

            var user = new EditUserViewModel();


            await Assert.ThrowsAsync<ArgumentException>(async () => await usersService.EditUser(user));
        }

        [Fact]
        public async void DeleteUserShoulThrowExeption()
        {
            var options = new DbContextOptionsBuilder<EClinicDbContext>()
                .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
                .Options;
            var dbContext = new EClinicDbContext(options);

            //var mockUserStore = new Mock<IUserStore<EClinicUser>>();
            //var mockRoleManager = new Mock<IUserRoleStore<EClinicUser>>();

            //var userManager = new UserManager<EClinicUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var userManager = MockHelpers.MockUserManager<EClinicUser>().Object;

            var examService = new Mock<ExamService>(dbContext).Object;
            var appointmentService = new Mock<AppointmentService>(dbContext).Object;
            var usersService = new Mock<UsersService>(dbContext, userManager, examService).Object;

            AutoMapperConfig.RegisterMappings(typeof(SetingViewModel).GetTypeInfo().Assembly);




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
            var exam = new CreateExamInputModel()
            {
                Condition = "good",
                DoctorUserName = "nelov87@gmail.com",
                Diagnose = "adss",
                PatientUserName = "nelov872@gmail.com",
                Prescription = "sdfsdd"
            };

            await examService.CreateExam(exam);




            var user = new EditUserViewModel();


            await Assert.ThrowsAsync<ArgumentException>(async () => await usersService.DeleteUser(""));
        }



    }

}
