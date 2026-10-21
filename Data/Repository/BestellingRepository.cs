namespace MVCDemo.Data.Repository
{
    public class BestellingRepository : GenericRepository<Bestelling>, IBestellingRepository
    {
        public BestellingRepository(MVCDemoContext context) : base(context) { }

        public async Task<IEnumerable<Bestelling>> GetBestellingenMetKlantEnProducten()
        {
            return await _context.Bestellingen
                .Include(x => x.Klant)
                .Include(x => x.Orderlijnen)
                .ThenInclude(x => x.Product)
                .ToListAsync();
        }

        public async Task<Bestelling?> GetBestellingMetKlantEnProducten(int id)
        {
            return await _context.Bestellingen
                .Include(x => x.Klant)
                .Include(x => x.Orderlijnen)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
