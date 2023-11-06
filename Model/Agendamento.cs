using CourtBooker.Enuns;
using System.ComponentModel.DataAnnotations;

namespace CourtBooker.Model
{
    public class Agendamento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O agendamento não possui tempo inícial")]
        public TimeSpan HorarioInicial { get; set; }
        [Required(ErrorMessage = "O agendamento não possui tempo final")]
        public TimeSpan HorarioFinal { get; set; }
        public DateTime Data { get; set; }
        [StringLength(11, ErrorMessage = "Formato do Cpf inválido", MinimumLength = 11)]
        [Required(ErrorMessage = "O campo Cpf é obrigatório e não pode ser deixado em branco")]
        public string CpfUsuario { get; set; }
        [Required(ErrorMessage = "O campo Quadra é obrigatório e não pode ser deixado em branco")]
        public int IdQUadra { get; set; }
        public EnumStatusAgendamento StatusAgendamento { get; set; }
        public string StatusAgendamentoAux => StatusAgendamento.ToString().ToLower();
        public string? EmailUsuario { get; set; }
        public bool Presenca { get; set; }
        public bool Evento { get; set; }
        public List<DiasSemana> DiasSemana { get; set; } = new();

    }
}
