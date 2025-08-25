using ResunetDAL.Models;

namespace ResunetDAL.Interfaces;

public interface IAuth
{
    Task<UserModel> GetUser(string email);
    
    Task<UserModel> GetUser(int id);
    
    Task<int> CreateUser(UserModel model); 
}

