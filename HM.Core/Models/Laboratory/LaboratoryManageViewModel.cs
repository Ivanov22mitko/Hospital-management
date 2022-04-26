using HM.Infrastructure.Data;

namespace HM.Core.Models.Laboratory
{
    public class LaboratoryManageViewModel
    {
        public LaboratoryManageViewModel()
        {
            Operators = new List<Doctor>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Doctor> Operators { get; set; }
    }
}
