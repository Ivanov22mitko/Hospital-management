using HM.Core.Contracts;
using HM.Core.Models.Referral;
using HM.Infrastructure.Data;
using HM.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HM.Core.Services
{
    public class ReferralService : IReferralService
    {
        private readonly IApplicationDbRepository repo;

        public ReferralService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public AddReferralViewModel GetReferral(string patientId, string doctorId)
        {
            var model = new AddReferralViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                DoctorId = doctorId,
                PatientId = patientId
            };

            return model;
        }

        public async Task<IEnumerable<ReferralListViewModel>> GetReferrals(string id)
        {
            var referrals = await repo.All<Referral>()
                .Where(r => r.PatientId == id)
                .ToListAsync();

            return referrals.
                Select(r => new ReferralListViewModel()
                {
                    FromDate = r.FromDate,
                    DueDate = r.DueDate,
                    Specialist = r.Specialist
                });
        }

        public async Task GiveReferralTo(AddReferralViewModel model)
        {
            var doctor = await repo.GetByIdAsync<Doctor>(model.DoctorId);
            var patient = await repo.GetByIdAsync<Patient>(model.PatientId);

            var referral = new Referral()
            {
                Id = model.Id,
                Specialist = model.Specialist,
                FromDate = model.FromDate,
                DueDate = model.DueDate,
                DoctorId = model.DoctorId,
                PatientId = model.PatientId,
                Doctor = doctor,
                Patient = patient
            };

            await repo.AddAsync(referral);
            await repo.SaveChangesAsync();
        }
    }
}
