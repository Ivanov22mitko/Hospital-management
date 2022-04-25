using HM.Core.Models;
using HM.Core.Models.Disease;

namespace HM.Core.Contracts
{
    public interface IDiseaseService
    {
        Task AddDiseaseToDb(AddDiseaseViewModel model);

        Task RemoveDiseaseFromDb(string id);

        Task<IEnumerable<DiseaseListViewModel>> GetDiseases();
    }
}
