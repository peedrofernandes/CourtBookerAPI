using CourtBooker.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtBooker.Model
{
    [Table("usuario")]
    public class User
    {
        [StringLength(11, ErrorMessage = "Cpf format is invalid", MinimumLength = 11)]
        [Required(ErrorMessage = "User must have a CPF")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "User must have a Password")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "User must have an Email")]
        public string Email { get; set; }

        public DateTime? DataFimBolsa { get; set; }

        [Range(0, 2)]
        public Enuns.UserType UserType { get; set; }
        public string UserTypeAux => UserType.ToString();


        //public User(string cpf, string senha, string email, UserType userType, DateTime? dataFimBolsa)
        //{
        //    Cpf = cpf;
        //    Senha = senha;
        //    Email = email;
        //    UserType = userType;
        //    DataFimBolsa = dataFimBolsa;
        //}
    }
}
