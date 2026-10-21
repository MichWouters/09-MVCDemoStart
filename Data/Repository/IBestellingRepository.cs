namespace MVCDemo.Data.Repository
{
    public interface IBestellingRepository : IGenericRepository<Bestelling>
    {
        Task<IEnumerable<Bestelling>> GetBestellingenMetKlantEnProducten();

        Task<Bestelling?> GetBestellingMetKlantEnProducten(int id);
    }
}
