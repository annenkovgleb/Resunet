using ResunetDAL.Models;

namespace ResunetDAL.Interfaces;

public interface ISkill
{
    Task<int> Create(string skillname);

    Task<IEnumerable<SkillModel>> Search(int top, string skillname);

    Task<SkillModel> Get(string skillname);

    Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId);

    Task<int> AddProfileSkill(ProfileSkillModel model);
}
