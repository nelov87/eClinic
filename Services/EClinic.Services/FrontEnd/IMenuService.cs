using EClinic.Web.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Text;

namespace EClinic.Services.FrontEnd
{
    public interface IMenuService
    {
        ICollection<MenuViewModel> GetAll();

        
    }
}
