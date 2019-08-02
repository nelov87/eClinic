using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EClinic.Data;
using EClinic.Services.Mapping;
using EClinic.Web.ViewModels.Site;

namespace EClinic.Services.FrontEnd
{
    public class MenuService : IMenuService
    {
        private readonly EClinicDbContext db;

        public MenuService(EClinicDbContext db)
        {
            this.db = db;
        }

        public ICollection<MenuViewModel> GetAll()
        {
            return this.db.SitePages.To<MenuViewModel>().ToHashSet();
        }

        
    }
}
