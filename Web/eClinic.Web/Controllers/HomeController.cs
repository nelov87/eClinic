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

        public HomeController(IMenuService menuService, IPageService pageService)
        {
            this.menuService = menuService;
            this.pageService = pageService;
        }

        public async Task<IActionResult> Index()
        {
            this.ViewData["MenuItems"] = this.menuService.GetAll();

            return View();
        }

        public async Task<IActionResult> GetPage(string id)
        {
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
