using ResunetDal.Models;

namespace ResunetBl.Profile
{
    public interface ISkill
    {
        Task<IEnumerable<SkillModel>> Search(int top, string skillname);
    }
}
