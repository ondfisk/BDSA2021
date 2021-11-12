namespace MyApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly ILogger<CharactersController> _logger;
    private readonly ICharacterRepository _repository;

    public CharactersController(ILogger<CharactersController> logger, ICharacterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<CharacterDto>> Get()
        => await _repository.ReadAsync();

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(CharacterDetailsDto), 200)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterDetailsDto>> Get(int id)
        => (await _repository.ReadAsync(id)).ToActionResult();

    [HttpPost]
    [ProducesResponseType(typeof(CharacterDetailsDto), 201)]
    public async Task<IActionResult> Post(CharacterCreateDto character)
    {
        var created = await _repository.CreateAsync(character);

        return CreatedAtRoute(nameof(Get), new { created.Id }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Put(int id, [FromBody] CharacterUpdateDto character)
        => (await _repository.UpdateAsync(id, character)).ToActionResult();

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
        => (await _repository.DeleteAsync(id)).ToActionResult();
}
