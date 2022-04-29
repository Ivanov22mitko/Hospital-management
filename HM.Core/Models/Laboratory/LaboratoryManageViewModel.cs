using HM.Infrastructure.Data;

namespace HM.Core.Models.Laboratory
{
    public class LaboratoryManageViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string[] Operators { get; set; }
    }
}
