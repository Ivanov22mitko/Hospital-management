namespace HM.Core.Models.Appointment
{
    public class AddAppointmentViewModel
    {
        public string Id { get; set; }

        public string PatientId { get; set; }

        public string DoctorId { get; set; }

        public DateTime AppointmentTime { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }
    }
}
