using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Identity;

namespace HM.Core.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetDoctors();

        Task PopulateEntities(string role, ApplicationUser user);
    }
}
