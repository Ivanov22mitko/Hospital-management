using HM.Core.Models.Laboratory;
using HM.Infrastructure.Data;

namespace HM.Core.Contracts
{
    public interface ILaboratoryService
    {
        Task<IEnumerable<LaboratoryListViewModel>> GetLaboratories();

        Task AddLaboratoryToDb(AddLaboratoryViewModel model);

        Task<LaboratoryManageViewModel> ManageLaboratory(string id);

        Task<Laboratory> GetLaboratoryById(string id); 

        Task<bool> UpdateLaboratory(LaboratoryManageViewModel model);
    }
}
