namespace HM.Core.Models.Referral
{
    public class AddReferralViewModel
    {
        public string Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime DueDate { get; set; }

        public string PatientId { get; set; }

        public string DoctorId { get; set; }

        public string Specialist { get; set; }
    }
}
