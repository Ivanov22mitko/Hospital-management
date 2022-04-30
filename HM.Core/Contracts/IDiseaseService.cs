using HM.Core.Models;
using HM.Core.Models.Disease;
using HM.Infrastructure.Data;

namespace HM.Core.Contracts
{
    public interface IDiseaseService
    {
        Task AddDiseaseToDb(AddDiseaseViewModel model);

        Task RemoveDiseaseFromDb(string id);

        Task<IEnumerable<DiseaseListViewModel>> GetDiseases();

        Task<IEnumerable<string>> GetDiseasesList();

        Task<Disease> GetDiseaseByName(string name);

        IEnumerable<string>? GetPatientDiseases(Patient patient);
    }
}
