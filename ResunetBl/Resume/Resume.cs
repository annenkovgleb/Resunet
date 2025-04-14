using ResunetDAL.Interfaces;
using ResunetDAL.Models;

namespace ResunetBl.Resume
{
    public class Resume(IProfile profile) : IResume
    {
        public async Task<ResumeModel> Get(int profileId)
        {
            ProfileModel profileModel = await profile.GetByProfileId(profileId);
            return new ResumeModel
            {
                Profile = profileModel
            };
        }

        public async Task<IEnumerable<ProfileModel>> Search(int top)
        {
            return await profile.Search(top);
        }
    }
}
