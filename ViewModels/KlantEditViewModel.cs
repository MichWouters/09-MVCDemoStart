namespace MVCDemo.ViewModels
{
    public class KlantEditViewModel
    {
        // Grootste verschil met een Create Model -> Wel een ID Property
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam Verplicht")]
        [MinLength(3, ErrorMessage = "Minstens 3 karakters")]
        [Display(Name = "Achternaam")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Naam Verplicht")]
        [MinLength(3, ErrorMessage = "Minstens 3 karakters")]
        [MaxLength(100, ErrorMessage = "Gast. Veel te lang")]
        public string Voornaam { get; set; }

        [Required]
        [Display(Name = "Datum aangemaakt")]
        public DateTime AangemaaktDatum { get; set; } = DateTime.Now;

        public bool IsMeerderjarig { get; set; } = true;
    }
}
