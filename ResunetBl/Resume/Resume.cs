using ResunetDAL.Interfaces;
using ResunetDAL.Models;

namespace ResunetBl.Resume
{
    public class Resume(IProfileDAL _profileDAL) : IResume
    {
        public async Task<ResumeModel> Get(int profileId)
        {
            ProfileModel profileModel = await _profileDAL.GetByProfileId(profileId);
            return new ResumeModel
            {
                Profile = profileModel
            };
        }

        public async Task<IEnumerable<ProfileModel>> Search(int top)
        {
            return await _profileDAL.Search(top);
        }
    }
}
