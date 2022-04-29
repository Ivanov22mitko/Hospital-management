using HM.Core.Contracts;
using HM.Core.Models.Appointment;
using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HM.Core.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IApplicationDbRepository repo;

        public AppointmentService(IApplicationDbRepository _repo)
        {
            repo = _repo;
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
    }
}
