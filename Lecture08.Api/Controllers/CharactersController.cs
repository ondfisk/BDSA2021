using System.Collections.Generic;
using System.Threading.Tasks;
using Lecture08.Api.Model;
using Lecture08.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lecture08.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IReadOnlyCollection<CharacterDTO>> Get()
            => await _repository.ReadAsync();

        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(CharacterDetailsDTO), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => (await _repository.ReadAsync(id)).ToActionResult();

        [HttpPost]
        [ProducesResponseType(typeof(CharacterDetailsDTO), 201)]
        public async Task<IActionResult> Post(CharacterCreateDTO character)
        {
            var created = await _repository.CreateAsync(character);

            return CreatedAtRoute(nameof(Get), new { created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(int id, [FromBody] CharacterUpdateDTO character)
            => (await _repository.UpdateAsync(character)).ToActionResult();

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
            => (await _repository.DeleteAsync(id)).ToActionResult();
    }
}
