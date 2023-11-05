using System.ComponentModel;

namespace CourtBooker.Enuns
{
    public enum EnumStatusAgendamento
    {
        [Description("reservado")]
        RESERVADO,
        [Description("cancelado")]
        CANCELADO,
        [Description("finalizado")]
        FINALIZADO
    }
}
