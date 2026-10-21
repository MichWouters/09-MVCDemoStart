namespace MVCDemo.Data.Repository
{
    public class KlantRepository : GenericRepository<Klant>, IKlantRepository
    {
        public KlantRepository(MVCDemoContext context) : base(context) { }

        public async Task<List<Klant>> GetKlantenMetBestellingen()
        {
            return await _context.Klanten
                .Include(x => x.Bestellingen)
                .ThenInclude(x => x.Orderlijnen)
                .ThenInclude(x => x.Product)
                .ToListAsync();
        }

        public async Task<Klant?> GetKlantMetBestellingen(int id)
        {
            return await _context.Klanten
                .Include(x => x.Bestellingen)
                .ThenInclude(x => x.Orderlijnen)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> KlantBestaat(string naam, string voornaam)
        {
            return _context.Klanten.AnyAsync(k => k.Naam == naam && k.Voornaam == voornaam);
        }
    }
}
