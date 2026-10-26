namespace MVCDemo.ViewModels
{
    public class KlantListViewModel
    {
        public List<KlantViewModel> Klanten { get; set; } = new List<KlantViewModel>();

        public string ZoekWaarde { get; set; } = string.Empty;
    }
}
