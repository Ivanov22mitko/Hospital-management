using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HM.Infrastructure.Data
{
    public class Referral
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }

        [Required]
        public string PatientId { get; set; }

        [Required]
        public string Specialist { get; set; }

        [Required]
        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; }

        [Required]
        public string DoctorId { get; set; }
    }
}
