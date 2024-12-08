using Dapper;
using Npgsql;

namespace ResunetDal
{
    public class DbHelper
    {
        public static string ConnString = "User ID=postgres;Password=password;Host=localhost;Port=5432;Database=dev";

        public static async Task ExecuteAsync(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(ConnString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, model);
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(ConnString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<T>(sql, model);
            }
        }

        public static async Task<T> QueryScalarAsync<T>(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(ConnString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<T>(sql, model);
            }
        }
    }
}

