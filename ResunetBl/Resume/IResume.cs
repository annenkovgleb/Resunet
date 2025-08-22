using ResunetDal.Models;

namespace ResunetBl.Resume
{
    public interface IResume
    {
        Task<IEnumerable<ProfileModel>> Search(int top);

        Task<ResumeModel> Get(int profileId);
    }
}
