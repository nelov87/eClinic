namespace eClinic.Web.Controllers
{
    using eClinic.Services;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ISiteService siteService;

        public HomeController(ISiteService siteService)
        {
            this.siteService = siteService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult AboutUs()
        {
            var page = this.siteService.GetContent("About Us");
            return this.View(page);
        }

        public IActionResult Services()
        {
            return this.View();
        }

        public IActionResult Contacts()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
