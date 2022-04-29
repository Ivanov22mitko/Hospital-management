using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HM.Infrastructure.Data
{
    public class Appointment
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string PatientId { get; set; }

        [Required]
        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }

        [Required]
        public string DoctorId { get; set; }

        [Required]
        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }

        [Required]
        [StringLength(100)]
        public string Reason { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
