using HM.Core.Contracts;
using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HM.Core.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IApplicationDbRepository repo;

        public DoctorService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await repo.All<Doctor>().ToListAsync();
        }
    }
}
