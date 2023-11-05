namespace CourtBooker.Model
{
    public class Quadra
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdBloco { get; set; }
        public List<int> IdTiposEsporte { get; set; } = new();
    }
}
