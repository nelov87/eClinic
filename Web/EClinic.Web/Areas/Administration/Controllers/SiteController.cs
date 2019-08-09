using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EClinic.Services;
using EClinic.Web.ViewModels.Site;
using EClinic.Web.InputModels;
using Microsoft.AspNetCore.Authorization;
using EClinic.Common;

namespace EClinic.Web.Areas.Administration.Controllers
{
    
    public class SiteController : AdministrationController
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
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            var pages = await this.pageService.GetAllPages();

            return this.View(pages);
        }

        //public async Task<IActionResult> IndexPageSlidesAll()
        //{
        //    if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
        //    {
        //        return this.Redirect("/");
        //    }

        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> EditPage(string id)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            this.ViewData["Page"] = await this.pageService.GetPage(id);

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> EditPage(PageInputModel pageInput)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"~/Administration/Site/GetAllPages");
            }

            try
            {
                await this.pageService.EditPage(pageInput);
            }
            catch (Exception e)
            {
                return this.Redirect("~/Administration/Site/GetAllPages");
            }

            return this.Redirect("~/Administration/Site/GetAllPages");
        }

        [HttpGet]
        public async Task<IActionResult> AddPage()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPage(NewPageInputModel pageInput)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(pageInput);
            }

            try
            {
                await this.pageService.AddPage(pageInput);
            }
            catch (Exception e)
            {
                return this.View(pageInput);
            }

            return this.Redirect("~/Administration/Site/GetAllPages");
        }

        [HttpGet]
        public async Task<IActionResult> DeletePage(string id)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            try
            {
                await this.pageService.DeletePage(id);
            }
            catch (Exception e)
            {
                return this.Redirect("~/Administration/Site/GetAllPages");
            }
            

            return this.Redirect("~/Administration/Site/GetAllPages");
        }

        //[HttpGet]
        //public async Task<IActionResult> EditSlide(int id)
        //{
        //    if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
        //    {
        //        return this.Redirect("/");
        //    }

        //    return this.View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditSlide()
        //{
        //    if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
        //    {
        //        return this.Redirect("/");
        //    }

        //    return this.View();
        //}

        public async Task<IActionResult> Setings()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            List<SetingViewModel> setings = this.settingsService.GetAll().ToList();
            this.ViewData["SetingsList"] = setings;

            return this.View();
        }

        
        [HttpPost]
        public async Task<IActionResult> EditSeting(SetingsInputModel setingsInputModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            if (!this.ModelState.IsValid)
            {
                return this.Redirect("Setings");
            }

            try
            {
                this.settingsService.EditSeting(setingsInputModel.Id, setingsInputModel.Value);
            }
            catch (Exception e)
            {
                return this.View(setingsInputModel);
            }

            return this.Redirect("~/Administration/Site/Setings/");
        }
    }
}