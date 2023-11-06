using CourtBooker.Enuns;
using System.ComponentModel.DataAnnotations;

namespace CourtBooker.Model
{
    public class Evento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Evento não possui tempo inícial")]
        public TimeSpan HorarioInicial { get; set; }
        [Required(ErrorMessage = "O Evento não possui tempo final")]
        public TimeSpan HorarioFinal { get; set; }
        [Required(ErrorMessage = "O Evento não possui tempo inícial")]
        public DateTime DataInicial { get; set; }
        [Required(ErrorMessage = "O Evento não possui tempo inícial")]
        public DateTime DataFinal { get; set; }
        [StringLength(11, ErrorMessage = "Formato do Cpf inválido", MinimumLength = 11)]
        [Required(ErrorMessage = "O campo Cpf é obrigatório e não pode ser deixado em branco")]
        public string CpfUsuario { get; set; }
        [Required(ErrorMessage = "O Evento deve conter uma descrição")]
        public string Descricao { get; set; }
    }
}
