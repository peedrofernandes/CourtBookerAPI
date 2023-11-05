using System.ComponentModel.DataAnnotations;

namespace CourtBooker.Model
{
    public class Esporte
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Sport Name field is required and cannot be left blank")]
        public string Nome { get; set; }
        public List<int> IdQuadras { get; set; } = new();
    }
}
