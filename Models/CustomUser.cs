

namespace MVCDemo.Models
{
    public class CustomUser: IdentityUser
    {
        [PersonalData]
        public string? Naam { get; set; }

        [PersonalData]
        public string? VoorNaam { get; set; }

        [PersonalData]
        public DateTime? Geboortedatum { get; set; }
    }
}
