using CourtBooker.Model;
using Dapper;
using Npgsql;
using System.Data;

namespace CourtBooker.Services
{
    public class UserService
    {
        private static readonly string ConnectionString = "User ID=postgres;Password=sa@123;Host=localhost;Port=5432;Database=CourtBooker;";

        public static List<User> ListarUsuarios()
        {
            IDbConnection con;
            try
            {
                string sql = "SELECT cpf_usuario AS Cpf, Email FROM usuario";
                con = new NpgsqlConnection(ConnectionString);
                con.Open();
                List<User> listaUsuarios = con.Query<User>(sql).ToList();
                con.Close();
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
