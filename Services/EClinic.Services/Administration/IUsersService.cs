using EClinic.Web.ViewModels.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EClinic.Services.Administration
{
    public interface IUsersService
    {
        Task<List<UserViewModel>> GetAllUsers();

        Task<EditUserViewModel> GetUser(string email);
    }
}
