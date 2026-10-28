using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDemo.ViewModels;

public class BestellingController : Controller
{
    private IUnitOfWork _uow;

    public BestellingController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IActionResult> Index()    
    {
        return View(await _uow.BestellingRepository.GetAllAsync());
    }

    public async Task<IActionResult> Create()
    {
        BestellingCreateViewModel vm = new BestellingCreateViewModel();
        await VulSelectLists(vm);

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BestellingCreateViewModel vm)
    {
        if(!ModelState.IsValid)
        {
            // Als de view opnieuw getoond wordt,
            // moeten de SelectLists opnieuw gevuld worden
            await VulSelectLists(vm);
            return View(vm);
        }

        Bestelling bestelling = vm.Adapt<Bestelling>();

        try
        {
            await _uow.BestellingRepository.AddAsync(bestelling);
            await _uow.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            return BadRequest(ex);
        }


        return RedirectToAction(nameof(Index));
    }

    private async Task VulSelectLists(BestellingCreateViewModel vm)
    {
        var klanten = await _uow.KlantRepository.GetAllAsync();
        vm.Klanten = new SelectList(klanten, "Id", "Naam");

        var producten = await _uow.ProductRepository.GetAllAsync();
        vm.Producten = new SelectList(producten, "Id", "Naam");
    }
}
