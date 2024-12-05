using Resunet.DAL.Models;
using Resunet.ViewModels;

namespace Resunet.ViewMapper
{
    public class SkillMapper
    {
        public static ProfileSkillModel MapSkillViewModelToProfileSkillModel(SkillViewModel model)
        {
            return new ProfileSkillModel()
            {
                SkillName = model.Name,
                Level = model.Level,
                ProfileId = model.ProfileId
            };
        }
    }
}
