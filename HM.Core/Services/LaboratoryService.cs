using HM.Core.Contracts;
using HM.Core.Models.Laboratory;
using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HM.Core.Services
{
    public class LaboratoryService : ILaboratoryService
    {
        private readonly IApplicationDbRepository repo;

        public LaboratoryService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddLaboratoryToDb(AddLaboratoryViewModel model)
        {
            var laboratory = new Laboratory()
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description
            };

            await repo.AddAsync<Laboratory>(laboratory);
            await repo.SaveChangesAsync();
        }

        public async Task<Laboratory> GetLaboratoryById(string id)
        {
            return await repo.GetByIdAsync<Laboratory>(id);
        }

        public async Task<LaboratoryManageViewModel> ManageLaboratory(string id)
        {
            var laboratory = await repo.GetByIdAsync<Laboratory>(id);

            return new LaboratoryManageViewModel()
            {
                Id = laboratory.Id,
                Name = laboratory.Name,
                Description = laboratory.Description,
                Operators = laboratory.Operators
            };
        }

        public async Task<bool> UpdateLaboratory(LaboratoryManageViewModel model)
        {
            bool result = false;
            var laboratory = await repo.GetByIdAsync<Laboratory>(model.Id);

            if (laboratory != null)
            {
                laboratory.Name = model.Name;

                laboratory.Description = model.Description;

                laboratory.Operators = model.Operators;

                await repo.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        async Task<IEnumerable<LaboratoryListViewModel>> ILaboratoryService.GetLaboratories()
        {
            return await repo.All<Laboratory>()
                .Select(l => new LaboratoryListViewModel()
                {
                    Id = l.Id,
                    Name = l.Name,
                    Description = l.Description
                })
                .ToListAsync();
        }
    }
}
