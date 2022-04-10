using HM.Core.Contracts;
using HM.Core.Models;
using HM.Infrastructure.Data.Identity;
using HM.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HM.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;

        public UserService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}",
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<UserEditViewModel> UserEdit(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            return new UserEditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
