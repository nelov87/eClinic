using EClinic.Services;
using EClinic.Services.FrontEnd;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EClinic.Web.ViewComponents
{
    public class HeaderComponent : ViewComponent
    {
        private readonly IMenuService menuService;
        private readonly ISettingsService settingsService;

        public HeaderComponent(IMenuService menuService, ISettingsService settingsService)
        {
            this.menuService = menuService;
            this.settingsService = settingsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            this.ViewData["SiteName"] = this.settingsService.GetSiteName();
            this.ViewData["PhoneNumber"] = this.settingsService.GetTelefon();
            this.ViewData["Email"] = this.settingsService.GetTelefon();

            return View(this.menuService.GetAll());
        }
    }
}
