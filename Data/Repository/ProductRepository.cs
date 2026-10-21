
using Microsoft.EntityFrameworkCore;

namespace MVCDemo.Data.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MVCDemoContext context) : base(context) { }

        public async Task<IEnumerable<Product>> SearchAsync(string zoekwaarde)
        {
            return await _context
                .Producten
                .Where(x => x.Naam.Contains(zoekwaarde))
                .OrderBy(x => x.Naam)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> productIds)
        {
            return await _context
                .Producten
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();     
        }
    }
}
