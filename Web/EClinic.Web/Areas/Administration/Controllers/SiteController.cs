using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EClinic.Services;
using EClinic.Web.ViewModels.Site;
using EClinic.Web.InputModels;

namespace EClinic.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class SiteController : Controller
    {
        private readonly ISettingsService settingsService;
        private readonly IPageService pageService;

        public SiteController(ISettingsService settingsService, IPageService pageService)
        {
            this.settingsService = settingsService;
            this.pageService = pageService;
        }

        public async Task<IActionResult> GetAllPages()
        {
            var pages = await this.pageService.GetAllPages();

            return this.View(pages);
        }

        public async Task<IActionResult> IndexPageSlidesAll()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditPage(string id)
        {
             this.ViewData["Page"] = await this.pageService.GetPage(id);

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> EditPage(PageInputModel pageInput)
        {
            await this.pageService.EditPage(pageInput);

            return this.Redirect("~/Administration/Site/GetAllPages");
        }

        [HttpGet]
        public async Task<IActionResult> AddPage()
        {
            
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPage(PageInputModel pageInput)
        {
            await this.pageService.AddPage(pageInput);

            return this.Redirect("~/Administration/Site/GetAllPages");
        }

        [HttpGet]
        public async Task<IActionResult> DeletePage(string id)
        {
            await this.pageService.DeletePage(id);

            return this.Redirect("~/Administration/Site/GetAllPages");
        }

        [HttpGet]
        public async Task<IActionResult> EditSlide(int id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> EditSlide()
        {
            return this.View();
        }

        public async Task<IActionResult> Setings()
        {
            List<SetingViewModel> setings = this.settingsService.GetAll().ToList();
            this.ViewData["SetingsList"] = setings;

            return this.View();
        }

        
        [HttpPost]
        public async Task<IActionResult> EditSeting(SetingsInputModel setingsInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("Setings");
            }

            this.settingsService.EditSeting(setingsInputModel.Id, setingsInputModel.Value);

            return this.Redirect("~/Administration/Site/Setings/");
        }
    }
}