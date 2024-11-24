using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public interface IProfileDAL
    {
        Task<IEnumerable<ProfileModel>> Get(int userId);
        Task<int> Add(ProfileModel profile);
        Task Update(ProfileModel profile);
    }
}
