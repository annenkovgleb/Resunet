using System;
using System.ComponentModel.DataAnnotations;

namespace Resunet.BL.Auth
{
	public interface IAuthBL
	{
		Task<int> CreateUser(Resunet.DAL.Models.UserModel user);

		Task<int> Authenticate(string email, string password, bool rememberMe);

		Task<ValidationResult?> ValidateEmail(string email);
    }
}

