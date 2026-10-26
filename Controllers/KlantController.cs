using MVCDemo.ViewModels;
using Mapster;

namespace MVCDemo.Controllers
{
    public class KlantController : Controller
    {
        private ILogger<HomeController> _logger;
        private IUnitOfWork _uow;

        public KlantController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            // Haal data op met Uow en Repositories
            IEnumerable<Klant> klanten = await _uow.KlantRepository.GetAllAsync();

            // Map model naar een object dat geschikt is voor de klant
            KlantListViewModel vm = new KlantListViewModel
            {
                Klanten = klanten.Adapt<List<KlantViewModel>>()
            };

            // Verstuur gemapt object naar View
            return View(vm);
        }

        public IActionResult Details(int id)
        {
            // Controller voorziet een model
            KlantViewModel vm = new KlantViewModel
            {
                // Controller doet business logica om ViewModel te vullen
                AangemaaktDatum = DateTime.Now,
                Voornaam = "Michiel",
                Naam = "Wouters"
            };

            // Controller geeft VM aan de view
            return View(vm);
        }
    }
}
