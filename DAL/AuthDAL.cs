using System;
using Dapper;
using Npgsql;
using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public class AuthDAL : IAuthDAL
    {
        public async Task<UserModel> GetUser(string email)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                // OpenAsync - не будет держать ресурсы потока
                // сильно на производительность не повлияет 
                // только в те моменты, когда в pull чего-то не хватает и нужно подтянуть что-то
                // открыть доп соединение, но это будет редко
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                        select UserId, Email, Password, Salt, Status
                        from AppUser
                        where Email = @email", new { email = email }) ?? new UserModel(); 
                // если вернем нового пользователя, то у него будет пустой пароль и в AuthBL мы пытаемся хешировать null
            }
        }

        public async Task<UserModel> GetUser(int id)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                // если запись не найдена в бд => возвращается пустой объект 
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                        select UserId, Email, Password, Salt, Status
                        from AppUser
                        where UserId = @id", new { id = id }) ?? new UserModel();
            }
        }

        public async Task<int> CreateUser(UserModel model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                await connection.OpenAsync();
                string sql = @"insert into AppUser(Email, Password, Salt, Status)
                        values(@Email, @Password, @Salt, @Status) returning UserId";
                return await connection.QuerySingleAsync<int>(sql, model);
            }
        }
    }
}

