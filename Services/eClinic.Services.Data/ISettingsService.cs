using eClinic.Data.Models;
using eClinic.Web.ViewModels.Site;
using System.Collections.Generic;

namespace eClinic.Services.Data
{
    public interface ISettingsService
    {
        int GetCount();

        IEnumerable<SetingViewModel> GetAll();
    }
}
