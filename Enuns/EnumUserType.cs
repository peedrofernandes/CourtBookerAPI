using System.ComponentModel;

namespace CourtBooker.Enuns
{
    public enum UserType
    {
        [Description("comum")]
        comum,
        [Description("administrador")]
        administrador,
        [Description("bolsista")]
        bolsista
    }
}
