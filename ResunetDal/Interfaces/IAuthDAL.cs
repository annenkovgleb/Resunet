using ResunetDal.Models;

namespace ResunetDal.Interfaces
{
    public interface IAuthDAL
    {
        // поиск будет по email и id
        Task<UserModel> GetUser(string email);
        Task<UserModel> GetUser(int id);
        Task<int> CreateUser(UserModel model); // возвращает id созданного пользователя 
    }
}

