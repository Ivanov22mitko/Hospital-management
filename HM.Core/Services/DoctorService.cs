using HM.Core.Contracts;
using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Identity;
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

        public async Task PopulateEntities(string role, ApplicationUser user)
        {
            if (role == "Patient")
            {
                await repo.AddAsync<Patient>(new Patient()
                {
                    Id= user.Id,
                    FirstName= user.FirstName,
                    LastName= user.LastName,
                    Email= user.Email,
                    PhoneNumber = user.PhoneNumber
                });
            }

            else if (role == "Doctor")
            {
                await repo.AddAsync<Doctor>(new Doctor()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Specialization = "Add specialization"
                });
            }

            else
            {
                throw new ArgumentException("Invalid role.");
            }
        }
    }
}
