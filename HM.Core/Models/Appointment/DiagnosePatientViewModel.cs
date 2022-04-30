using Microsoft.AspNetCore.Mvc.Rendering;

namespace HM.Core.Models.Appointment
{
    public class DiagnosePatientViewModel
    {
        public string PatientId { get; set; }

        public string PatientName { get; set; }

        public string Disease { get; set; }

        public IEnumerable<SelectListItem>? Diseases { get; set; }
    }
}
