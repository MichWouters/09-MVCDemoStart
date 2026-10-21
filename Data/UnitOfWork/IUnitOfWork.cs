using MVCDemo.Data.Repository;

namespace MVCDemo.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBestellingRepository BestellingRepository { get; }
	    IKlantRepository KlantRepository { get; }
        IProductRepository ProductRepository { get; }

        IGenericRepository<Orderlijn> OrderlijnRepository { get; }
        public Task SaveChangesAsync();
    }
}
