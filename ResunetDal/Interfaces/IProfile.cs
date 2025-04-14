using ResunetDAL.Models;

namespace ResunetDAL.Interfaces;

public interface IProfile
{
    Task<IEnumerable<ProfileModel>> GetByUserId(int userId);
    
    Task<ProfileModel> GetByProfileId(int userId);
    
    Task<int> Add(ProfileModel profile);
    
    Task Update(ProfileModel profile);
    
    Task<IEnumerable<ProfileModel>> Search(int top);
}
