using HM.Core.Models.Appointment;
using HM.Infrastructure.Data;

namespace HM.Core.Contracts
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Doctor>> GetDoctorsGP();

        Task<IEnumerable<AppointmentListViewModel>> GetDoctorAppointments(string doctorId);

        Task<IEnumerable<PatientAppointmentListViewModel>> GetPatientAppointments(string patientId);

        Task AddAppointmentToDb(AddAppointmentViewModel model);

        Task CompleteAppointment(string id);

        Task RemoveAppointment(string id);

        Task<DiagnosePatientViewModel> GetDiagnosePatient(string id);

        Task SetDiagnose(DiagnosePatientViewModel model);
    }
}
