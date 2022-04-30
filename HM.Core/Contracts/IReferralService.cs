using HM.Core.Models.Referral;

namespace HM.Core.Contracts
{
    public interface IReferralService
    {
        Task GiveReferralTo(AddReferralViewModel model);

        Task<IEnumerable<ReferralListViewModel>> GetReferrals(string id);

        AddReferralViewModel GetReferral(string patientId, string doctorId);
    }
}
