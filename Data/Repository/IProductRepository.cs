namespace MVCDemo.Data.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> SearchAsync(string zoekwaarde);

        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> productIds);
    }
}
