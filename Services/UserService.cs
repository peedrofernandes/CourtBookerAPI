using CourtBooker.Model;
using Dapper;
using Npgsql;
using System.Data;

namespace CourtBooker.Services
{
    public class UserService : BaseRepository
    {
        public List<User> ListarUsuarios()
        {
            try
            {
                return WithConnection(dbConn =>
                {
                    string sql = "SELECT cpf_usuario AS Cpf, Email, hashsenha AS Senha, tipo as UserType, data_final_bolsa AS DataFimBolsa FROM usuario";
                    return dbConn.Query<User>(sql).ToList();
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public User? GetUser(string cpf)
        {
            return WithConnection(dbConn =>
            {
                string sql = "SELECT cpf_usuario AS Cpf, Email, hashsenha AS Senha, tipo as UserType, data_final_bolsa AS DataFimBolsa FROM usuario";
                sql += " WHERE cpf_usuario = @Cpf";
                return dbConn.QueryFirstOrDefault<User>(sql, new {Cpf = cpf});
            });
        }

        public bool AddUser(User user)
        {
            return WithConnection(dbConn =>
            {
                string sql = "INSERT INTO usuario (cpf_usuario, email, hashsenha, tipo, data_final_bolsa) VALUES (@Cpf, @Email, @Senha,CAST(@UserTypeAux AS tipo_usuario), @DataFimBolsa)";
                int rowsAffected = dbConn.Execute(sql, user);
                return rowsAffected > 0;
            });
        }

        public bool PutUser(User user)
        {
            if (GetUser(user.Cpf) == null)
                throw new BadHttpRequestException("User not Found. Cpf is invalid");

            return WithConnection(dbConn =>
            {
                string sql = "UPDATE usuario SET hashsenha = @Senha, data_final_bolsa = @DataFimBolsa, email = @Email, tipo = CAST(@Tipo AS tipo_usuario) WHERE cpf_usuario = @Cpf";
                int rowsAffected = dbConn.Execute(sql, user);
                return rowsAffected > 0;
            });
        }

        public bool DeleteUser(string cpf)
        {
            return WithConnection(dbConn =>
            {
                string sql = "Delete from usuario WHERE cpf_usuario = @Cpf";
                int rowsAffected = dbConn.Execute(sql, new {Cpf = cpf});
                return rowsAffected > 0;
            });
        }
    }
}
