using System.ComponentModel;

namespace CourtBooker.Enuns
{
    public enum TipoUsuario
    {
        [Description("comum")]
        comum,
        [Description("administrador")]
        administrador,
        [Description("bolsista")]
        bolsista
    }
}
