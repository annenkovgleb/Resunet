using Resunet.DAL.Models;

namespace Resunet.BL.Profile
{
    public interface IProfile
    {
        Task<IEnumerable<ProfileModel>> Get(int userId);

        Task AddOrUpdate(ProfileModel model); 
        
        Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId);
        
        Task AddProfileSkill(ProfileSkillModel model);
    }
}
