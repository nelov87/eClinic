using EClinic.Data;
using EClinic.Services;
using EClinic.Services.Exams;
using EClinic.Services.FrontEnd;
using EClinic.Services.Mapping;
using EClinic.Web.Controllers;
using EClinic.Web.ViewModels.Site;
using Microsoft.AspNetCore.Identity.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Reflection;
using Xunit;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using EClinic.Data.Models;

namespace EClinic.Controlers.Tests
{
    public class HomeControlerTests
    {
        [Fact]
        public async void IndexControllerGetPageWhitNonExistingIdShuldReturnRedirectResult()
        {
            var options = new DbContextOptionsBuilder<EClinicDbContext>()
                .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
                .Options;
            var dbContext = new EClinicDbContext(options);

            //var mockUserStore = new Mock<IUserStore<EClinicUser>>();
            //var mockRoleManager = new Mock<IUserRoleStore<EClinicUser>>();

            //var userManager = new UserManager<EClinicUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            //var userManager = MockHelpers.MockUserManager<EClinicUser>().Object;

            var menuService = new Mock<MenuService>(dbContext).Object;
            var pageService = new Mock<PageService>(dbContext).Object;
            var settingsService = new Mock<SettingsService>(dbContext).Object;

            AutoMapperConfig.RegisterMappings(typeof(SetingViewModel).GetTypeInfo().Assembly);

            var homeController = new HomeController(menuService, pageService, settingsService);

            await dbContext.Settings.AddAsync(new Setting { Name = "Site Name", Value = "eClinic" });
            await dbContext.Settings.AddAsync(new Setting { Name = "Phone Number", Value = "+123 987 887 765" });
            await dbContext.Settings.AddAsync(new Setting { Name = "E-Mail", Value = "sales@smarteyeapps.com" });

            dbContext.SaveChanges();

            var result = await homeController.GetPage("sddsdds");






            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async void IndexControllerGetPageWhitNoIdShuldReturnRedirectResult()
        {

            // Arrange
            var options = new DbContextOptionsBuilder<EClinicDbContext>()
                .UseInMemoryDatabase(databaseName: "Appointment_CreateAppointment")
                .Options;
            var dbContext = new EClinicDbContext(options);

            //var mockUserStore = new Mock<IUserStore<EClinicUser>>();
            //var mockRoleManager = new Mock<IUserRoleStore<EClinicUser>>();

            //var userManager = new UserManager<EClinicUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            //var userManager = MockHelpers.MockUserManager<EClinicUser>().Object;

            var menuService = new Mock<MenuService>(dbContext).Object;
            var pageService = new Mock<PageService>(dbContext).Object;
            var settingsService = new Mock<SettingsService>(dbContext).Object;

            AutoMapperConfig.RegisterMappings(typeof(SetingViewModel).GetTypeInfo().Assembly);

            var homeController = new HomeController(menuService, pageService, settingsService);

            await dbContext.Settings.AddAsync(new Setting { Name = "Site Name", Value = "eClinic" });
            await dbContext.Settings.AddAsync(new Setting { Name = "Phone Number", Value = "+123 987 887 765" });
            await dbContext.Settings.AddAsync(new Setting { Name = "E-Mail", Value = "sales@smarteyeapps.com" });

            dbContext.SaveChanges();


            // Act
            var result = await homeController.GetPage("");

            // Assert
            Assert.IsType<RedirectResult>(result);
        }
    }
}
