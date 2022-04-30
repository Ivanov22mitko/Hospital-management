using HM.Core.Contracts;
using HM.Core.Models;
using HM.Core.Models.Disease;
using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HM.Core.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IApplicationDbRepository repo;

        public DiseaseService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddDiseaseToDb(AddDiseaseViewModel model)
        {
            var disease = new Disease()
            {
                Id = Guid.NewGuid().ToString(),
                ICD = model.ICD,
                Name = model.Name
            };

            await repo.AddAsync<Disease>(disease);
            await repo.SaveChangesAsync();
        }

        public async Task<Disease> GetDiseaseByName(string name)
        {
            return await repo.All<Disease>()
                .Where(d => d.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DiseaseListViewModel>> GetDiseases()
        {
            return await repo.All<Disease>()
                .Select(d => new DiseaseListViewModel()
                {
                    Id = d.Id,
                    ICD = d.ICD,
                    Name = d.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetDiseasesList()
        {
            return await repo.All<Disease>()
                .Select(d => d.Name)
                .ToArrayAsync();
        }

        public IEnumerable<string>? GetPatientDiseases(Patient patient)
        {
            if (patient.Diseases == null)
            {
                return null;
            }
            else
            {
                return patient.Diseases
                .Select(d => d.Name)
                .ToArray();
            }          
        }

        public async Task RemoveDiseaseFromDb(string id)
        {
            await repo.DeleteAsync<Disease>(id);
            await repo.SaveChangesAsync();
        }
    }
}
