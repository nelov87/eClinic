using EClinic.Services.Administration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EClinic.Web.ViewComponents
{
    public class ProfilePictureViewComponent : ViewComponent
    {
        private readonly IUsersService usersService;

        public ProfilePictureViewComponent(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string email)
        {
            var img = await this.usersService.GetUserProfilePicture(email);

            this.ViewData["ProfilePicture"] = img;

            return this.View();
        }
        //private Task<List<TodoItem>> GetItemsAsync(int maxPriority, bool isDone)
        //{
        //    return db.ToDo.Where(x => x.IsDone == isDone &&
        //                         x.Priority <= maxPriority).ToListAsync();
        //}

    }
}
