using CourtBooker.Enuns;
using CourtBooker.Helpers;
using CourtBooker.Model;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourtBooker.Services
{
    public class AgendamentoService : BaseRepository
    {
        private QuadraService _quadraService = new QuadraService();
        public Agendamento AdicionarAgendamento(Agendamento agendamento)
        {
            return WithConnection(dbConn =>
            {
                string sql = GetQuery(agendamento);
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
                return dbConn.Query<Agendamento>(sql, new { IdBloco = idBloco }).ToList();
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

        public bool AdicionarEvento(Agendamento agendamento)
        {
            return WithConnection(dbConn =>
            {
                string sql = GetQuery(agendamento);
                int rowsAffected = dbConn.Execute(sql);
                return rowsAffected > 0;
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
            return "SELECT a.id, emailUsuario, cpf_usuario AS CpfUsuario, id_quadra AS IdQuadra, status as StatusAgendamento, dataInicial as DataInicio, dataFinal as DataFim, " +
                "horario_inicial AS HorarioInicial, horario_final as HorarioFinal, presenca FROM agendamento a";
        }
        public static List<Agendamento> GerarEventosSemanais(Agendamento agendamento)
        {
            var eventos = new List<Agendamento>();

            for (var dt = agendamento.DataInicio; dt <= agendamento.DataFim; dt = dt.AddDays(1))
            {
                if (agendamento.DiasSemana.Contains((int)dt.DayOfWeek))
                {
                    agendamento.DataInicio = dt;
                    eventos.Add(new Agendamento(
                      agendamento.HorarioInicial,
                      agendamento.HorarioFinal,
                      dt,
                      agendamento.DataFim,
                      agendamento.CpfUsuario,
                      agendamento.IdQUadra,
                      agendamento.StatusAgendamento,
                      agendamento.EmailUsuario,
                      agendamento.Presenca,
                      agendamento.Evento,
                      agendamento.Recorrente,
                      agendamento.DiasSemana
                    ));
                }
            }

            return eventos;
        }
        private static string GetQuery(Agendamento agendamento)
        {
            string sql;
            if (agendamento.Evento && agendamento.Recorrente)
            {
                List<Agendamento> lista = GerarEventosSemanais(agendamento);
                sql = CriarComandosInsert(lista);
            }
            else
                sql = CriarComandosInsert(new List<Agendamento> { agendamento });

            return sql;
        }

        public static string CriarComandosInsert(List<Agendamento> eventos)
        {
            var comandos = new List<string>();

            foreach (var evento in eventos)
            {
                string diasSemanaArray = "{" + string.Join(",", evento.DiasSemana.Select(d => ((int)d).ToString())) + "}";


                string comando = "INSERT INTO agendamento (cpf_usuario, id_quadra, status, dataInicial, dataFinal, horario_inicial, horario_final, emailUsuario, presenca, evento, recorrente, diasSemana) " +
                                 "VALUES (" +
                                 $"'{evento.CpfUsuario}', " +
                                 $"{evento.IdQUadra}, " +
                                 $"CAST('{evento.StatusAgendamentoAux}' AS status_agendamento), " +
                                 $"'{evento.DataInicio.ToString("yyyy-MM-dd")}', " +
                                 $"'{evento.DataFim.ToString("yyyy-MM-dd")}', " +
                                 $"'{evento.HorarioInicial}', " +
                                 $"'{evento.HorarioFinal}', " +
                                 $"'{evento.EmailUsuario}', " +
                                 $"{(evento.Presenca ? "TRUE" : "FALSE")}, " +
                                 $"{(evento.Evento ? "TRUE" : "FALSE")}, " +
                                 $"{(evento.Recorrente ? "TRUE" : "FALSE")}, " +
                                 $"'{diasSemanaArray}'" +
                                 ")";
                if (eventos.Count == 1)
                    comando += " RETURNING *";
                else
                    comando += ";";

                comandos.Add(comando);
            }

            return string.Join(" ", comandos);
        }

        public dynamic ValidarAgendamento(Agendamento agendamento)
        {
            Quadra? quadra = _quadraService.BuscarQuadra(agendamento.IdQUadra);

            if (quadra == null)
                throw new BadHttpRequestException("Quadra indisponível");

            CustomHelper.IsValidEmail(agendamento.EmailUsuario);
            
            if (agendamento.Evento && agendamento.Recorrente)
                return AdicionarEvento(agendamento);
            else
                return AdicionarAgendamento(agendamento);

        }

        public void GetEmailMessage(Agendamento agendamento, out string message, out string receiver, out string subject, bool cancelar)
        {
            Quadra quadra = _quadraService.BuscarQuadra(agendamento.IdQUadra);

            receiver = agendamento.EmailUsuario;
            subject = cancelar ? "Cancelamento Agendamento" : "Confirmação Agendamento";
            message = cancelar ? "Agendamento Cancelado\n\n" : "Agendamento realizado com sucesso\n\n";
            message += $"Data Agendamento: {agendamento.DataInicio.ToString("dd/MM/yyyy")}\n";
            message += $"Horário: {agendamento.HorarioInicial} - {agendamento.HorarioFinal}\n";
            message += $"Quadra: {quadra.Nome}";
        }

        public void GetCancelationEmailMessage(Agendamento agendamento, out string message, out string receiver, out string subject)
        {
            Quadra quadra = new QuadraService().BuscarQuadra(agendamento.IdQUadra);

            receiver = agendamento.EmailUsuario;
            subject = "Cancelamento Agendamento";
            message = "Agendamento Cancelado!\n\n";
            message += $"Data Agendamento: {agendamento.DataInicio:yyyy-mm-dd}\n";
            message += $"Horário: {agendamento.HorarioInicial} - {agendamento.HorarioFinal}\n";
            message += $"Quadra: {quadra.Nome}";
        }
    }
}
