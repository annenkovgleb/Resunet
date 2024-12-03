using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public interface IProfileDAL
    {
        Task<IEnumerable<ProfileModel>> GetByUserId(int userId);
        Task<ProfileModel> GetByProfileId(int userId);
        Task<int> Add(ProfileModel profile);
        Task Update(ProfileModel profile);
        Task<IEnumerable<ProfileModel>> Search(int top);
    }
}
