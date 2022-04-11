using System.ComponentModel.DataAnnotations;

namespace HM.Infrastructure.Data
{
    public class Patient
    {
        public Patient()
        {
            Appointments = new List<Appointment>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Number { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; }

        public IEnumerable<Disease> Diseases { get; set; }
    }
}
