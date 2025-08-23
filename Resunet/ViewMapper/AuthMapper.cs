using Resunet.ViewModels;
using ResunetDAL.Models;

namespace Resunet.ViewMapper;

public static class AuthMapper
{
    // из RegisterViewModel получаем модель и превращаем ее в модель UserModel и отправлить на DAL-уровень
    public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model)
        => new UserModel()
        {
            Email = model.Email!,
            Password = model.Password!
        };
}