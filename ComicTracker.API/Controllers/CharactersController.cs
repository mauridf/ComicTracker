using ComicTracker.Application.DTOs.ComicVine;
using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ComicTracker.Domain.Entities;

namespace ComicTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceResponse<List<ComicVineCharacter>>>> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new ServiceResponse<List<ComicVineCharacter>>
                {
                    Success = false,
                    Message = "Name parameter is required"
                });
            }

            var response = await _characterService.SearchCharacters(name);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceResponse<Character>>> Create(CharacterCreateDto characterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ServiceResponse<Character>
                {
                    Success = false,
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var response = await _characterService.CreateCharacter(characterDto);

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<Character>>> GetById(int id)
        {
            var response = await _characterService.GetCharacterById(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> GetAll()
        {
            var response = await _characterService.GetAllCharacters();
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<Character>>> Update(int id, CharacterUpdateDto characterDto)
        {
            if (id != characterDto.Id)
            {
                return BadRequest(new ServiceResponse<Character>
                {
                    Success = false,
                    Message = "ID mismatch"
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ServiceResponse<Character>
                {
                    Success = false,
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var response = await _characterService.UpdateCharacter(characterDto);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _characterService.DeleteCharacter(id);

            if (!response.Success)
                return NotFound(response);

            return NoContent();
        }

        [HttpGet("comicvine/{comicVineId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<Character>>> GetByComicVineId(int comicVineId)
        {
            var response = await _characterService.GetCharacterByComicVineId(comicVineId);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}