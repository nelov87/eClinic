using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EClinic.Data;
using EClinic.Services.Mapping;
using EClinic.Web.ViewModels.Site;



namespace EClinic.Services
{
    public class SiteService : ISiteService
    {
        private readonly EClinicDbContext db;

        public SiteService(EClinicDbContext db)
        {
            this.db = db;
        }

        public void EditPage(string page)
        {
            
        }

        public PageViewModel GetContent(string page)
        {
            var content = this.db.SitePages.Where(x => x.Title == page).To<PageViewModel>().FirstOrDefault();

            return content;
        }
    }
}
