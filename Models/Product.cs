

namespace MVCDemo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string? Beschrijving { get; set; }
        public decimal Prijs { get; set; }
        public List<Orderlijn> Orderlijnen { get; set; } = default!;
    }
}
