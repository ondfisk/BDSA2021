namespace MyApp.Mvc.Controllers;

public class CharactersController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICharacterRepository _repository;

    public CharactersController(ILogger<HomeController> logger, ICharacterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    // GET: CharactersController
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var characters = await _repository.ReadAsync();

        return View(characters);
    }

    // GET: CharactersController/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var character = await _repository.ReadAsync(id);

        return character == null ? NotFound() : View(character);
    }

    // GET: CharactersController/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: CharactersController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CharacterCreateDto character)
    {
        if (!ModelState.IsValid)
        {
            return View(character);
        }

        var created = await _repository.CreateAsync(character);

        return RedirectToAction(nameof(Details), new { created.Id });
    }

    // GET: CharactersController/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var character = await _repository.ReadAsync(id);

        return character == null ? NotFound() : View(character);
    }

    // POST: CharactersController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CharacterUpdateDto character)
    {
        if (!ModelState.IsValid)
        {
            return View(character);
        }

        var status = await _repository.UpdateAsync(character);

        if (status != Status.Updated)
        {
            ModelState.AddModelError("", status.ToString());
            return View(character);
        }
        else
        {
            return RedirectToAction(nameof(Details), new { id });
        }
    }

    // GET: CharactersController/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var character = await _repository.ReadAsync(id);

        return character == null ? NotFound() : View(character);
    }

    // POST: CharactersController/Delete/5
    [ActionName(nameof(Delete))]
    [HttpDelete]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteExecute(int id)
    {
        var status = await _repository.DeleteAsync(id);

        if (status != Status.Deleted)
        {
            ModelState.AddModelError("", status.ToString());
            var character = await _repository.ReadAsync(id);
            return View(character);
        }
        else
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
