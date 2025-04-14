using ResunetDAL.Interfaces;
using ResunetDAL.Models;

namespace ResunetBl.Profile
{
    public class Skill(ISkillDAL _skillDAL) : ISkill
    {
        public async Task<IEnumerable<SkillModel>> Search(int top, string skillName)
        {
            return await _skillDAL.Search(top, skillName);
        }
    }
}
