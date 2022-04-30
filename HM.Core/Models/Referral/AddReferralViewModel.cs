using System.ComponentModel.DataAnnotations;

namespace HM.Core.Models.Referral
{
    public class AddReferralViewModel
    {
        public string Id { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public string PatientId { get; set; }

        [Required]
        public string DoctorId { get; set; }

        [Required]
        public string Specialist { get; set; }
    }
}
