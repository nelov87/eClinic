using EClinic.Web.InputModels;
using EClinic.Web.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EClinic.Services
{
    public interface IPageService
    {
        Task<ICollection<PageViewModel>> GetAllPages();

        Task<PageViewModel> GetPage(string id);

        Task<bool> EditPage(PageInputModel pageInput);
    }
}
