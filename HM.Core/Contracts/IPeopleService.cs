using HM.Core.Models;
using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Identity;

namespace HM.Core.Contracts
{
    public interface IPeopleService
    {
        Task<IEnumerable<Doctor>> GetDoctors();

        Task PopulateEntities(string role, ApplicationUser user);

        Task<bool> UpdateDoctor(UserEditViewModel model);

        Task<bool> UpdatePatient(UserEditViewModel model);

        Task UpdateInfoDoctor(ApplicationUser user);

        Task UpdateInfoPatient(ApplicationUser user);
    }
}
