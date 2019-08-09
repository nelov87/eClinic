using EClinic.Data;
using EClinic.Services.Mapping;
using EClinic.Web.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EClinic.Services
{
    public class SettingsService : ISettingsService
    {
        
        private readonly EClinicDbContext db;

        public SettingsService(EClinicDbContext db)
        {
            this.db = db;
            
        }

        public void EditSeting(int id, string value)
        {
            var seting = this.db.Settings.FirstOrDefault(x => x.Id == id);
            seting.Value = value;
            this.db.SaveChanges();

        }

        public IEnumerable<SetingViewModel> GetAll()
        {
            var setings = this.db.Settings.Where(x => true).To<SetingViewModel>().ToList();
            return setings;
        }

        public int GetCount()
        {
            return this.db.Settings.Count();
        }

        public string GetSiteName()
        {
            var name = this.db.Settings.FirstOrDefault(x => x.Name == "Site Name").Value;

            return name;
        }

        public string GetTelefon()
        {
            var name = this.db.Settings.FirstOrDefault(x => x.Name == "Phone Number").Value;

            return name;
        }

        public string GetEmail()
        {
            var name = this.db.Settings.FirstOrDefault(x => x.Name == "E-Mail").Value;

            return name;
        }
    }
}
