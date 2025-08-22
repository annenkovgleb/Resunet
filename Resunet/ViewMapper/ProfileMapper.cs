using Resunet.ViewModels;
using ResunetDAL.Models;

namespace Resunet.ViewMapper;

public static class ProfileMapper
{
    public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
        => new ProfileModel()
        {
            ProfileId = model.ProfileId,
            ProfileName = model.ProfileName,
            FirstName = model.FirstName,
            LastName = model.LastName,
            ProfileStatus = model.ProfileStatus
        };

    // превращение бэкенд во фронтенд
    public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
        => new ProfileViewModel()
        {
            ProfileId = model.ProfileId,
            ProfileName = model.ProfileName,
            FirstName = model.FirstName,
            LastName = model.LastName,
            ProfileImage = model.ProfileImage,
            ProfileStatus = model.ProfileStatus
        };
}