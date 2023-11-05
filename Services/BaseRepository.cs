using Npgsql;
using System.Data;

namespace CourtBooker.Services
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString = "User ID=postgres;Password=sa@123;Host=localhost;Port=5432;Database=CourtBooker;";

        protected BaseRepository()
        {
        }

        protected T WithConnection<T>(Func<IDbConnection, T> getData)
        {
            try
            {
                using IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
                dbConnection.Open();
                return getData(dbConnection);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao precessar no banco de dados: {ex.Message}");
            }
        }
    }

}
