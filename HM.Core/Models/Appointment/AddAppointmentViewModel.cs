using System.ComponentModel.DataAnnotations;

namespace HM.Core.Models.Appointment
{
    public class AddAppointmentViewModel
    {
        public string Id { get; set; }

        [Required]
        public string PatientId { get; set; }

        [Required]
        public string DoctorId { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Reason should be no more than 100 characters.")]
        public string Reason { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
