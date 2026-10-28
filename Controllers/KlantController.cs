using MVCDemo.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Index(KlantListViewModel viewModel)
        {
            IEnumerable<Klant> klanten;
            // Haal data op met Uow en Repositories

            if (string.IsNullOrWhiteSpace(viewModel.ZoekWaarde))
            {
                klanten = await _uow.KlantRepository.GetAllAsync();
            }
            else
            {
                // Search string is niet leeg. Zoek naar (een) bepaalde klant(en)
                klanten = await _uow.KlantRepository.SearchKlanten(viewModel.ZoekWaarde);
            }

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

        // HttpGet -> Haalt formulier op
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KlantCreateViewModel vm)
        {
            // Business specifieke validaties
            if (await _uow.KlantRepository.KlantBestaat(vm.Naam, vm.Voornaam))
            {
                ModelState.AddModelError("", "Klant bestaat al");
            }

            // Map VM naar Model
            Klant klant = vm.Adapt<Klant>();

            try
            {
                // Query maken
                await _uow.KlantRepository.AddAsync(klant);

                // Query uitvoeren
                await _uow.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                ModelState.AddModelError("DB", "Er is een fout opgetreden");
                _logger.LogError(ex.Message);
            }

            // Stuur de gebruiker terug naar de GET variant
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            Klant klant = await _uow.KlantRepository.GetByIdAsync(id);

            if (klant == null)
            {
                return NotFound();
            }

            KlantEditViewModel vm = klant.Adapt<KlantEditViewModel>();

            // Go to app.com/Klant/Edit/X
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KlantEditViewModel vm)
        {
            if (id != vm.Id)
            {
                ModelState.AddModelError("Id", "De Id's komen niet overeen");
                return View(vm);
            }

            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            Klant bestaandeKlant = await _uow.KlantRepository.GetByIdAsync(id);

            if (bestaandeKlant == null)
            {
                ModelState.AddModelError("Klant", $"Klant {id} bestaat niet!");
                return View(vm);
            }

            vm.Adapt<Klant>();

            try
            {
                _uow.KlantRepository.Update(bestaandeKlant);
                await _uow.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                _logger.LogError(e.Message);

                if (!await _uow.KlantRepository.ExistsAsync(id))
                {
                    ModelState.AddModelError("Klant", "Er is een probleem opgetreden bij het wegschrijven naar de database.");
                    return View(vm);
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        
    }
}
