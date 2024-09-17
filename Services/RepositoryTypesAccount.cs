using BudgetApp.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BudgetApp.Services
{
    public interface IRespositoryTypesAccount
    {
        Task Create(TypeAccount typeAccount);
        Task<bool> Exist(string name, int userId);
        Task<IEnumerable<TypeAccount>> ObtainAccount(int userId);
    }
    public class RepositoryTypesAccount : IRespositoryTypesAccount
    {
        private readonly string connectionString;
        public RepositoryTypesAccount(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(TypeAccount typeAccount)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"
                                                INSERT INTO AccountTypes (name, userId, orderBy) 
                                                VALUES (@name, @userId, @orderBy);
                                                SELECT SCOPE_IDENTITY();", typeAccount);
            typeAccount.Id = id;
        }

        public async Task<bool> Exist(string name, int userId)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>(
                                                                        @" SELECT 1 
                                                                        FROM AccountTypes
                                                                        WHERE name = @name AND userId = @userId",
                                                                        new { name, userId });
            return exist == 1;
        }

        public async Task<IEnumerable<TypeAccount>> ObtainAccount(int userId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TypeAccount>(
                                                             @"SELECT id, name, orderBy
                                                             FROM AccountTypes
                                                             WHERE userId = @userId",
                                                             new { userId });
        }
    }
}
