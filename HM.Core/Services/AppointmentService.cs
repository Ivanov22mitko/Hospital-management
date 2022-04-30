using HM.Core.Contracts;
using HM.Core.Models.Appointment;
using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HM.Core.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IApplicationDbRepository repo;

        private readonly IDiseaseService diseaseService;

        public AppointmentService(IApplicationDbRepository _repo,
            IDiseaseService _diseaseService)
        {
            repo = _repo;
            diseaseService = _diseaseService;
        }

        public async Task AddAppointmentToDb(AddAppointmentViewModel model)
        {
            var appointment = new Appointment()
            {
                Id = model.Id,
                Reason = model.Reason,
                Status = model.Status,
                AppointmentTime = model.AppointmentTime,
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                Doctor = await repo.GetByIdAsync<Doctor>(model.DoctorId),
                Patient = await repo.GetByIdAsync<Patient>(model.PatientId)
            };

            await repo.AddAsync(appointment);
            await repo.SaveChangesAsync();
        }

        public async Task CompleteAppointment(string id)
        {
            var appointment = await repo.GetByIdAsync<Appointment>(id);
            appointment.Status = "Completed";

            repo.Update<Appointment>(appointment);
            await repo.SaveChangesAsync();
        }

        public async Task<DiagnosePatientViewModel> GetDiagnosePatient(string id)
        {
            var patient = await repo.GetByIdAsync<Patient>(id);

            var patientDiseses = diseaseService.GetPatientDiseases(patient);

            var possibleDiseases = await diseaseService.GetDiseasesList();

            var diseases = possibleDiseases.Select(d => new SelectListItem()
            {
                Text = d,
                Value = d,
                Disabled = (patientDiseses == null) ? false : !patientDiseses.Contains(d)
            });

            return new DiagnosePatientViewModel()
            {
                PatientId = patient.Id,
                PatientName = $"{patient.FirstName} {patient.LastName}",
                Diseases = diseases
            };
        }

        public async Task SetDiagnose(DiagnosePatientViewModel model)
        {
            var patient = await repo.GetByIdAsync<Patient>(model.PatientId);

            var disease = await diseaseService.GetDiseaseByName(model.Disease);

            patient.Diseases.Append<Disease>(disease);

            repo.Update<Patient>(patient);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<AppointmentListViewModel>> GetDoctorAppointments(string doctorId)
        {
            return await repo.All<Appointment>()
                .Where(a => a.DoctorId == doctorId)
                .Select(a => new AppointmentListViewModel()
                {
                    Id = a.Id,
                    Reason = a.Reason,
                    Date = a.AppointmentTime,
                    PatientName = $"{a.Patient.FirstName} {a.Patient.LastName}",
                    PatientId = a.Patient.Id
                }).ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsGP()
        {
            return await repo.All<Doctor>()
                .Where(d => d.Specialization == "GP")
                .ToListAsync();
        }

        public async Task<IEnumerable<PatientAppointmentListViewModel>> GetPatientAppointments(string patientId)
        {
            return await repo.All<Appointment>()
                .Where(a => a.PatientId == patientId)
                .Select(a => new PatientAppointmentListViewModel()
                {
                    Reason = a.Reason,
                    Date = a.AppointmentTime,
                    DoctorName = $"{a.Doctor.FirstName} {a.Doctor.LastName}",
                    Status = a.Status
                }).ToListAsync();
        }

        public async Task RemoveAppointment(string id)
        {
            await repo.DeleteAsync<Appointment>(id);
            await repo.SaveChangesAsync();
        }

        public AddAppointmentViewModel SetAppointment(string patientId)
        {
            var appointment = new AddAppointmentViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                PatientId = patientId,
                Status = "Pending"
            };

            return appointment;
        }
    }
}
