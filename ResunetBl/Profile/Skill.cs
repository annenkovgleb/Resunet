using ResunetDAL.Models;

namespace ResunetBl.Profile;

public class Skill(ResunetDAL.Interfaces.ISkill skill) : ISkill
{
    public async Task<IEnumerable<SkillModel>> Search(int top, string skillName)
        => await skill.Search(top, skillName);
}