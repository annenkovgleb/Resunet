using Resunet.DAL.Models;

namespace Resunet.BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(Resunet.DAL.Models.UserModel user);

        Task<int> Authenticate(string email, string password, bool rememberMe);

        Task ValidateEmail(string email);
        
        Task Register(UserModel user);
    }
}

