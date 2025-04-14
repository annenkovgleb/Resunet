using ResunetDAL.Models;

namespace ResunetDAL.Interfaces;

public interface IAuth
{
    //get user by email or id
    Task<UserModel> GetUser(string email);
    
    Task<UserModel> GetUser(int id);
    
    Task<int> CreateUser(UserModel model); 
}

