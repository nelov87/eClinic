using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eClinic.Services;
using eClinic.Services.Data;
using eClinic.Web.ViewModels.Site;
using Microsoft.AspNetCore.Mvc;

namespace eClinic.Web.Areas.Administration.Controllers
{
    public class SiteController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly ISiteService siteService;

        public SiteController(ISettingsService settingsService, ISiteService siteService)
        {
            this.settingsService = settingsService;
            this.siteService = siteService;
        }

        public IActionResult IndexPageSlidesAll()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditSlide(int id)
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult EditSlide()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult EditAboutUs()
        {
           
            return this.View();
        }
        [HttpPost]
        public IActionResult EditAboutUs(int id)
        {
            var content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer neque libero, pulvinar et elementum quis, facilisis eu ante. Mauris non placerat sapien. Pellentesque tempor arcu non odio scelerisque ullamcorper. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam varius eros consequat auctor gravida. Fusce tristique lacus at urna sollicitudin pulvinar. Suspendisse hendrerit ultrices mauris.Ut ultricies lacus a rutrum mollis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Sed porta dolor quis felis pulvinar dignissim.Etiam nisl ligula, ullamcorper non metus vitae, maximus efficitur mi. Vivamus ut ex ullamcorper, scelerisque lacus nec, commodo dui.Proin massa urna, volutpat vel augue eget, iaculis tristique dui. ";

            this.ViewData["Content"] = content;
            return this.View();
        }

        public IActionResult EditContacts()
        {
            
            return this.View();
        }

        [HttpPost]
        public IActionResult EditContacts(int id)
        {
            
            return this.View();
        }

        public IActionResult Setings()
        {
            List<SetingViewModel> setings = this.settingsService.GetAll().ToList();

            return this.View(setings);
        }
    }
}