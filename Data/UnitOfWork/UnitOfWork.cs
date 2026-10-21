
namespace MVCDemo.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVCDemoContext _context;

        private IBestellingRepository bestellingRepository;
        private IKlantRepository klantRepository;
        private IProductRepository productRepository;

        private IGenericRepository<Orderlijn> orderlijnRepository;

        public UnitOfWork(MVCDemoContext context)
        {
            _context = context;
        }

        public IBestellingRepository BestellingRepository => bestellingRepository ??= new BestellingRepository(_context);

        public IKlantRepository KlantRepository => klantRepository ??= new KlantRepository(_context);

        public IProductRepository ProductRepository => productRepository ??= new ProductRepository(_context);

        public IGenericRepository<Orderlijn> OrderlijnRepository => orderlijnRepository ??= new GenericRepository<Orderlijn>(_context);


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
