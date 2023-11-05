using CourtBooker.Model;
using Dapper;

namespace CourtBooker.Services
{
    public class UsuarioService : BaseRepository
    {
        public List<Usuario> ListarUsuarios()
        {

            return WithConnection(dbConn =>
            {
                string sql = "SELECT cpf_usuario AS Cpf, Email, nome, hashsenha AS Senha, tipo as TipoUsuario, data_final_bolsa AS DataFimBolsa FROM usuario";
                return dbConn.Query<Usuario>(sql).ToList();
            });

        }
        public Usuario? BuscarUsuario(string cpf)
        {
            return WithConnection(dbConn =>
            {
                string sql = "SELECT cpf_usuario AS Cpf, Email, nome, hashsenha AS Senha, tipo as TipoUsuario, data_final_bolsa AS DataFimBolsa FROM usuario";
                sql += " WHERE cpf_usuario = @Cpf";
                return dbConn.QueryFirstOrDefault<Usuario>(sql, new { Cpf = cpf });
            });
        }

        public bool AdicionarUsuario(Usuario user)
        {
            return WithConnection(dbConn =>
            {
                string sql = "INSERT INTO usuario (cpf_usuario, nome, email, hashsenha, tipo, data_final_bolsa) VALUES (@Cpf, @Nome, @Email, @Senha, CAST(@TipoUsuarioAux AS tipo_usuario), @DataFimBolsa)";
                int rowsAffected = dbConn.Execute(sql, user);
                return rowsAffected > 0;
            });
        }

        public bool EditarUsuario(Usuario user)
        {
            if (BuscarUsuario(user.Cpf) == null)
                throw new BadHttpRequestException("User not Found. Cpf is invalid");

            return WithConnection(dbConn =>
            {
                string sql = "UPDATE usuario SET nome = @Nome, hashsenha = @Senha, data_final_bolsa = @DataFimBolsa, email = @Email, tipo = CAST(@TipoUsuarioAux AS tipo_usuario) WHERE cpf_usuario = @Cpf";
                int rowsAffected = dbConn.Execute(sql, user);
                return rowsAffected > 0;
            });
        }

        public bool ExcluirUsuario(string cpf)
        {
            return WithConnection(dbConn =>
            {
                string sql = "Delete from usuario WHERE cpf_usuario = @Cpf";
                int rowsAffected = dbConn.Execute(sql, new { Cpf = cpf });
                return rowsAffected > 0;
            });
        }

        public void ValidarUsuario(Usuario user)
        {
            if (string.IsNullOrEmpty(user.Cpf))
                throw new BadHttpRequestException("The Cpf field is required and cannot be left blank");

            if (string.IsNullOrEmpty(user.Nome))
                throw new BadHttpRequestException("The Name field is required and cannot be left blank");

            if (string.IsNullOrEmpty(user.Email))
                throw new BadHttpRequestException("The Email field is required and cannot be left blank");

            if (string.IsNullOrEmpty(user.Senha))
                throw new BadHttpRequestException("The Senha field is required and cannot be left blank");
        }
    }
}
