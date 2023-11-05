using CourtBooker.Model;
using Dapper;

namespace CourtBooker.Services
{
    public class QuadraService : BaseRepository
    {
        public List<Quadra> ListarQuadras()
        {
            return WithConnection(dbConn =>
            {
                string sql = "SELECT nome, id, id_bloco as IdBloco FROM quadra";
                return dbConn.Query<Quadra>(sql).ToList();
            });

        }

        public bool AdicionarQuadra(Quadra quadra)
        {
            return WithConnection(dbConn =>
            {
                string sql = "INSERT INTO quadra (nome, id_bloco) VALUES (@Nome, @IdBloco)";
                int rowsAffected = dbConn.Execute(sql, quadra);
                return rowsAffected > 0;
            });
        }

        public bool ExcluirQuadra(int id)
        {
            return WithConnection(dbConn =>
            {
                string sql = "Delete from quadra WHERE id = @Id";
                int rowsAffected = dbConn.Execute(sql, new { Id = id });
                return rowsAffected > 0;
            });
        }
    }
}
