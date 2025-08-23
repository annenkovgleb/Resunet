using ResunetDAL.Interfaces;
using ResunetDAL.Models;

namespace ResunetBl.Resume;

public class Resume(IProfile profile) : IResume
{
    public async Task<ResumeModel> Get(int profileId)
        => new ResumeModel
        {
            Profile = await profile.GetByProfileId(profileId)
        };

    public async Task<IEnumerable<ProfileModel>> Search(int top)
        => await profile.Search(top);
}