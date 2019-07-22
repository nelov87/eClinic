using eClinic.Web.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace eClinic.Services
{
    public interface ISiteService
    {
        PageViewModel GetContent(string page);

        void EditPage(string page);
    }
}
