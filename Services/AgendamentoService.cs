using CourtBooker.Enuns;
using CourtBooker.Helpers;
using CourtBooker.Model;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourtBooker.Services
{
    public class AgendamentoService : BaseRepository
    {
        public Agendamento AdicionarAgendamento(Agendamento agendamento)
        {
            ValidarAgendamento(agendamento);
            return WithConnection(dbConn =>
            {
                string sql = "INSERT INTO agendamento (cpf_usuario, id_quadra, status, data, horario_inicial, horario_final, emailUsuario, presenca)" +
                "VALUES (@CpfUsuario, @IdQuadra, CAST(@StatusAgendamentoAux AS status_agendamento), @Data, @HorarioInicial, @HorarioFinal, @EmailUsuario, @Presenca)" +
                " RETURNING *";
                return dbConn.QuerySingle<Agendamento>(sql, agendamento);
            });
        }

        public List<Agendamento> ListarAgendamentos()
        {

            return WithConnection(dbConn =>
            {
                string sql = QuerySelectAllAgendamento();
                return dbConn.Query<Agendamento>(sql).ToList();
            });

        }

        public List<Agendamento> ListarAgendamentosBloco(int idBloco)
        {
            return WithConnection(dbConn =>
            {
                string sql = QuerySelectAllAgendamento();
                sql += " join quadra q on id_quadra = q.id join bloco b on b.id = q.id_bloco where b.id = @IdBloco";
                return dbConn.Query<Agendamento>(sql, new {IdBloco = idBloco}).ToList();
            });

        }

        public bool ExcluirAgendamento(int id)
        {
            return WithConnection(dbConn =>
            {
                string sql = "Delete from agendamento WHERE id = @Id";
                int rowsAffected = dbConn.Execute(sql, new { Id = id });
                return rowsAffected > 0;
            });
        }

        public List<EnumValueDescription> ListarDiasSemana()
        {
            List<EnumValueDescription> result = EnumHelper.GetEnumValueDescriptionList<DiasSemana>();
            return result;
        }

        public Agendamento AdicionarEvento(Agendamento agendamento)
        {
            


            return WithConnection(dbConn =>
            {
                string sql = "INSERT INTO agendamento (cpf_usuario, id_quadra, status, data, horario_inicial, horario_final, emailUsuario, presenca)" +
                "VALUES (@CpfUsuario, @IdQuadra, CAST(@StatusAgendamentoAux AS status_agendamento), @Data, @HorarioInicial, @HorarioFinal, @EmailUsuario, @Presenca)" +
                " RETURNING *";
                return dbConn.QuerySingle<Agendamento>(sql, agendamento);
            });
        }

        //public bool EditarAgendamento(Agendamento agendamento)
        //{
        //    if (BuscarUsuario(user.Cpf) == null)
        //        throw new BadHttpRequestException("User not Found. Cpf is invalid");

        //    return WithConnection(dbConn =>
        //    {
        //        string sql = "UPDATE usuario SET nome = @Nome, hashsenha = @Senha, data_final_bolsa = @DataFimBolsa, email = @Email, tipo = CAST(@TipoUsuarioAux AS tipo_usuario) WHERE cpf_usuario = @Cpf";
        //        int rowsAffected = dbConn.Execute(sql, user);
        //        return rowsAffected > 0;
        //    });
        //}

        public Agendamento? BuscarAgendamento(int id)
        {
            return WithConnection(dbConn =>
            {
                string sql = QuerySelectAllAgendamento();
                sql += " WHERE id = @Id";
                return dbConn.QueryFirstOrDefault<Agendamento>(sql, new { Id = id });
            });
        }
        public static string QuerySelectAllAgendamento()
        {
            return "SELECT a.id, emailUsuario, cpf_usuario AS CpfUsuario, id_quadra AS IdQuadra, status as StatusAgendamento, data as Data, " +
                "horario_inicial AS HorarioInicial, horario_final as HorarioFinal, presenca FROM agendamento a";
        }

        private void ValidarAgendamento(Agendamento agendamento)
        {
            

        }
    }
}
