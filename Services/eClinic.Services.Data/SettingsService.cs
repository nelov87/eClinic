namespace eClinic.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using eClinic.Data;
    using eClinic.Data.Common.Repositories;
    using eClinic.Data.Models;
    using eClinic.Web.ViewModels.Site;
    using eClinic.Services.Mapping;

    public class SettingsService : ISettingsService
    {
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(ApplicationDbContext db, IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.db = db;
            this.settingsRepository = settingsRepository;
        }

        public IEnumerable<SetingViewModel> GetAll()
        {
            var setings = this.db.Settings.Where(x => true).To<SetingViewModel>().ToList();
            return setings;
        }

        public int GetCount()
        {
            return this.settingsRepository.All().Count();
        }
    }
}
