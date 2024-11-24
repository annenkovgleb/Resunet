using System;
using Resunet.ViewModels;
using Resunet.DAL.Models;

namespace Resunet.ViewMapper
{
	public class AuthMapper
	{
        // из RegisterViewModel будем получать модель и превращать ее в эту модель UserModel
        // и отправлять на DAL-уровень
        public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model)
		{
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}

