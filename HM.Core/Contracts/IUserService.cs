using HM.Core.Models;
using HM.Infrastructure.Data.Identity;

namespace HM.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserEditViewModel> UserEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

        Task<ApplicationUser> GetUserById(string id);

        Task PopulateEntities(string role, ApplicationUser user);
    }
}
