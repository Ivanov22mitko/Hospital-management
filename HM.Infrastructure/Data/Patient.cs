using System.ComponentModel.DataAnnotations;

namespace HM.Infrastructure.Data
{
    public class Patient
    {
        public Patient()
        {
            Appointments = new List<Appointment>();
            Referrals = new List<Referral>();
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
        [Phone]
        public string PhoneNumber { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; }

        public IEnumerable<Disease> Diseases { get; set; }

        public IEnumerable<Referral> Referrals { get; set; }
    }
}
