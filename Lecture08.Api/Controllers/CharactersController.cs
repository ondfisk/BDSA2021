using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lecture08.Core;
using Microsoft.AspNetCore.Mvc;

namespace Lecture08.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterRepository _repository;

        public CharactersController(ICharacterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<CharacterDTO>> Get() =>
            await _repository.ReadAsync();

        [HttpGet("{id}")]
        public async Task<CharacterDetailsDTO> Get(int id) =>
            await _repository.ReadAsync(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CharacterUpdateDTO character)
        {
            var response = await _repository.UpdateAsync(character);

            if (response == Lecture08.Core.Response.Updated)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
