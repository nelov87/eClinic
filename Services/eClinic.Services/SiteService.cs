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

 
        //public PageViewModel GetPage(string id)
        //{
        //    return this.db.SitePages.To<PageViewModel>().FirstOrDefault(x => x.Id == id);
        //}
    }
}
