namespace MVCDemo.ViewModels
{
    public class KlantViewModel
    {
        public int Id { get; set; }

        public string Naam { get; set; }

        public string Voornaam { get; set; }

        [Display(Name = "Klant sinds")]
        public DateTime AangemaaktDatum { get; set; }
    }
}
