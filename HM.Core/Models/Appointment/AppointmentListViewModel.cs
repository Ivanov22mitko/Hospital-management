namespace HM.Core.Models.Appointment
{
    public class AppointmentListViewModel
    {
        public string Id { get; set; }

        public string Reason { get; set; }

        public string PatientId { get; set; }

        public string PatientName { get; set; }

        public DateTime Date { get; set; }
    }
}
