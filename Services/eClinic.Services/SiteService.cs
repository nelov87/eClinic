namespace eClinic.Services
{
    using System;
    using System.Linq;

    using eClinic.Data;
    using eClinic.Services.Mapping;
    using eClinic.Web.ViewModels.Site;

    public class SiteService : ISiteService
    {
        private readonly ApplicationDbContext db;

        public SiteService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void EditPage(string page)
        {
            var a = 1;
        }

        public PageViewModel GetContent(string page)
        {
            var content = this.db.SitePages.Where(x => x.Title == page).To<PageViewModel>().FirstOrDefault();

            return content;
        }
    }
}
