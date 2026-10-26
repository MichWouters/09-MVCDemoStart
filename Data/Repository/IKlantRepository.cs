namespace MVCDemo.Data.Repository
{
    public interface IKlantRepository : IGenericRepository<Klant>
    {
        Task<List<Klant>> GetKlantenMetBestellingen();

        Task<Klant?> GetKlantMetBestellingen(int id);

        Task<bool> KlantBestaat(string naam, string voornaam);
        Task<IEnumerable<Klant>> SearchKlanten(string zoekWaarde);
    }
}
