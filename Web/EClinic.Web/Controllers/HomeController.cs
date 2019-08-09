using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EClinic.Web.Models;
using EClinic.Services.FrontEnd;
using EClinic.Services;

namespace EClinic.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuService menuService;
        private readonly IPageService pageService;
        private readonly ISettingsService settingsService;

        public HomeController(IMenuService menuService, IPageService pageService, ISettingsService settingsService)
        {
            this.menuService = menuService;
            this.pageService = pageService;
            this.settingsService = settingsService;
        }

        public async Task<IActionResult> Index()
        {
            this.ViewData["MenuItems"] = this.menuService.GetAll();
            this.ViewData["SiteName"] = this.settingsService.GetSiteName();
            this.ViewData["PhoneNumber"] = this.settingsService.GetTelefon();
            this.ViewData["Email"] = this.settingsService.GetTelefon();

            return View();
        }

        public async Task<IActionResult> GetPage(string id)
        {
            this.ViewData["MenuItems"] = this.menuService.GetAll();
            this.ViewData["SiteName"] = this.settingsService.GetSiteName();
            this.ViewData["PhoneNumber"] = this.settingsService.GetTelefon();
            this.ViewData["Email"] = this.settingsService.GetTelefon();

            var content = await this.pageService.GetPage(id);
            return this.View("../Home/Page", content);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
