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

        Task<List<string>> GetAllRoles();

        Task<bool> EditUser(EditUserViewModel viewModel);

        Task<bool> DeleteUser(string email);

        Task<List<UserViewModel>> SearchForUser(string username);


    }
}
