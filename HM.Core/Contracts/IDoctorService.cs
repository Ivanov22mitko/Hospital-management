using HM.Infrastructure.Data;

namespace HM.Core.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetDoctors();
    }
}
