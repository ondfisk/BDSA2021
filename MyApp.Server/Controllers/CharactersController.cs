namespace MyApp.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class CharactersController : ControllerBase
{
    private readonly ILogger<CharactersController> _logger;
    private readonly ICharacterRepository _repository;

    public CharactersController(ILogger<CharactersController> logger, ICharacterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IReadOnlyCollection<CharacterDto>> Get()
        => await _repository.ReadAsync();

    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CharacterDetailsDto), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterDetailsDto>> Get(int id)
        => (await _repository.ReadAsync(id)).ToActionResult();

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(CharacterDetailsDto), 201)]
    public async Task<IActionResult> Post(CharacterCreateDto character)
    {
        var created = await _repository.CreateAsync(character);

        return CreatedAtAction(nameof(Get), new { created.Id }, created);
    }

    [Authorize(Roles = $"{Member},{Administrator}")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] CharacterUpdateDto character)
            => (await _repository.UpdateAsync(id, character)).ToActionResult();

    [Authorize(Roles = Administrator)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
        => (await _repository.DeleteAsync(id)).ToActionResult();
}
