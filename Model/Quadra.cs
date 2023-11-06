namespace CourtBooker.Model
{
    public class Quadra
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdBloco { get; set; }
        public int[] IdTiposEsporte { get; set; }
    }
}
