using HM.Core.Contracts;
using HM.Core.Models;
using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Identity;
using HM.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HM.Core.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IApplicationDbRepository repo;

        public PeopleService(IApplicationDbRepository _repo)
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
                    Specialization = "GP"
                });
            }

            else
            {
                throw new ArgumentException("Invalid role.");
            }
        }

        public async Task<bool> UpdateDoctor(UserEditViewModel model)
        {
            bool result = false;
            var doctor = await repo.GetByIdAsync<Doctor>(model.Id);

            if (doctor != null)
            {
                doctor.FirstName = model.FirstName;
                doctor.LastName = model.LastName;
                doctor.Specialization = model.Specialization;

                await repo.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        public async Task UpdateInfoDoctor(ApplicationUser user)
        {
            var doctor = await repo.GetByIdAsync<Doctor>(user.Id);

            if (doctor != null)
            {
                doctor.FirstName = user.FirstName;
                doctor.LastName = user.LastName;

                await repo.SaveChangesAsync();
            }
        }

        public async Task UpdateInfoPatient(ApplicationUser user)
        {
            var patient = await repo.GetByIdAsync<Patient>(user.Id);

            if (patient != null)
            {
                patient.FirstName = user.FirstName;
                patient.LastName = user.LastName;
                patient.PhoneNumber = user.PhoneNumber;

                await repo.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdatePatient(UserEditViewModel model)
        {
            bool result = false;
            var patient = await repo.GetByIdAsync<Patient>(model.Id);

            if (patient != null)
            {
                patient.FirstName = model.FirstName;
                patient.LastName = model.LastName;

                await repo.SaveChangesAsync();

                result = true;
            }

            return result;
        }
    }
}
