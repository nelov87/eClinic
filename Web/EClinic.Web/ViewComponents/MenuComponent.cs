using EClinic.Services.FrontEnd;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EClinic.Web.ViewComponents
{
    public class MenuComponent : ViewComponent
    {
        private readonly IMenuService menuService;

        public MenuComponent(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            

            return View(this.menuService.GetAll());
        }
    }
}
