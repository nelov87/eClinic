using EClinic.Web.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace EClinic.Services
{
    public interface ISettingsService
    {
        int GetCount();

        IEnumerable<SetingViewModel> GetAll();

        void EditSeting(int id, string value);
    }
}
