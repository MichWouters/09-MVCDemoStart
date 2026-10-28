using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCDemo.ViewModels
{
    public class BestellingCreateViewModel
    {
        [Display(Name ="Kies een klant")]
        public int KlantId { get; set; }

        public List<OrderLijnCreateViewModel> OrderLijnen = new List<OrderLijnCreateViewModel>();

        public SelectList Klanten{ get; set; }

        public SelectList Producten { get; set; }
    }

    public class OrderLijnCreateViewModel
    {
        public int ProductId { get; set; }
        public int Aantal { get; set; }
    }
}
