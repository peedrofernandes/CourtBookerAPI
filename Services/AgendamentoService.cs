using CourtBooker.Model;
using Dapper;

namespace CourtBooker.Services
{
    public class AgendamentoService : BaseRepository
    {
        public Agendamento AdicionarAgendamento(Agendamento agendamento)
        {
            ValidarAgendamento(agendamento);
            return WithConnection(dbConn =>
            {
                string sql = "INSERT INTO agendamento (cpf_usuario, id_quadra, status, data, horario_inicial, horario_final)" +
                "VALUES (@CpfUsuario, @IdQuadra, CAST(@StatusAgendamentoAux AS status_agendamento), @Data, @HorarioInicial, @HorarioFinal)" +
                " RETURNING *";
                return dbConn.QuerySingle<Agendamento>(sql, agendamento);
            });
        }

        public List<Agendamento> ListarAgendamentos()
        {

            return WithConnection(dbConn =>
            {
                string sql = "SELECT cpf_usuario AS CpfUsuario, id_quadra AS IdQuadra, status as StatusAgendamento, data as Data, " +
                "horario_inicial AS HorarioInicial, horario_final as HorarioFinal FROM agendamento";
                return dbConn.Query<Agendamento>(sql).ToList();
            });

        }

        private void ValidarAgendamento(Agendamento agendamento)
        {
            Usuario? usuario = new UsuarioService().BuscarUsuario(agendamento.CpfUsuario);

            if (usuario == null)
                throw new BadHttpRequestException($"Usuário com o cpf {agendamento.CpfUsuario} não encontrado. Operação cancelada");

        }
    }
}
