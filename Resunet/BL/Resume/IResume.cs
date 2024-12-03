using Resunet.DAL.Models;

namespace Resunet.BL.Resume
{
    public interface IResume
    {
        Task<IEnumerable<ProfileModel>> Search(int top);

        Task<ResumeModel> Get(int profileId);
    }
}
