using System.ComponentModel.DataAnnotations.Schema;

namespace CourtBooker.Model
{
    [Table("usuario")]
    public class User
    {
        [Column("cpf_usuario")]
        public string Cpf { get; set; }
        
        [Column("hashsenha")]
        public string Senha { get; set; }
        
        public string  Email { get; set; }
        
        [Column("data_final_bolsa")]
        public DateTime DataFimBolsa { get; set; }

    }
}
