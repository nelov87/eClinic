using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EClinic.Services;
using EClinic.Web.ViewModels.Site;
<<<<<<< HEAD
using EClinic.Web.InputModels;
=======
>>>>>>> 0ceb752719ae7cab6b8164dd8918aaa8455e9c7f

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
        public async Task<IActionResult> EditSlide(int id)
        {
<<<<<<< HEAD
=======

>>>>>>> 0ceb752719ae7cab6b8164dd8918aaa8455e9c7f
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> EditSlide()
        {
            return this.View();
        }

        public async Task<IActionResult> Setings()
        {
<<<<<<< HEAD
            List<SetingViewModel> setings = this.settingsService.GetAll().ToList();
            this.ViewData["SetingsList"] = setings;
=======
>>>>>>> 0ceb752719ae7cab6b8164dd8918aaa8455e9c7f

            return this.View();
        }

        
        [HttpPost]
        public async Task<IActionResult> EditSeting(SetingsInputModel setingsInputModel)
        {
<<<<<<< HEAD
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("Setings");
            }

            this.settingsService.EditSeting(setingsInputModel.Id, setingsInputModel.Value);
=======

            return this.View();
        }

        public IActionResult Setings()
        {
            List<SetingViewModel> setings = this.settingsService.GetAll().ToList();
            this.ViewData["SetingsList"] = setings;

            return this.View();
        }

        [HttpPost]
        public IActionResult EditSeting(SetingViewModel setingViewModel)
        {
            this.settingsService.EditSeting(setingViewModel.Id, setingViewModel.Value);
>>>>>>> 0ceb752719ae7cab6b8164dd8918aaa8455e9c7f

            return this.Redirect("~/Administration/Site/Setings/");
        }
    }
}