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
        private readonly ILogger<PublishersController> _logger;
        private readonly ICharacterService _characterService;

        public CharactersController(ILogger<PublishersController> logger, ICharacterService characterService)
        {
            _logger = logger;
            _characterService = characterService;
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceResponse<List<ComicVineCharacter>>>> Search(string name)
        {
            _logger.LogInformation("Iniciando busca por Personagens com o nome: {name} na API da Comic Vine", name);
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("Não foi informado um nome para efetuar a pesquisa");
                return BadRequest(new ServiceResponse<List<ComicVineCharacter>>
                {
                    Success = false,
                    Message = "Name parameter is required"
                });
            }

            var response = await _characterService.SearchCharacters(name);
            _logger.LogInformation("Personagens com o nome {name} encontrados com sucesso", name);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceResponse<Character>>> Create(CharacterCreateDto characterDto)
        {
            _logger.LogInformation("Iniciando o registro de um novo Personagem");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Dado inválido");
                return BadRequest(new ServiceResponse<Character>
                {
                    Success = false,
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var response = await _characterService.CreateCharacter(characterDto);

            if (!response.Success)
            {
                _logger.LogWarning("Erro na gravação do Personagem");
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<Character>>> GetById(int id)
        {
            _logger.LogInformation("Iniciando a busca do Personagem com Id:{Id}", id);
            var response = await _characterService.GetCharacterById(id);
            _logger.LogInformation("Personagem id: {Id} localizado com sucesso", id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> GetAll()
        {
            _logger.LogInformation("Iniciando a busca de Todas os Personagens cadastrados");
            var response = await _characterService.GetAllCharacters();
            _logger.LogInformation("Todos os personagens cadastrados listados com sucesso");
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<Character>>> Update(int id, CharacterUpdateDto characterDto)
        {
            _logger.LogInformation("Iniciando a atualização dos dados do Personagem {Id}", id);
            if (id != characterDto.Id)
            {
                _logger.LogWarning("Erro na atualização do Personagem");
                return BadRequest(new ServiceResponse<Character>
                {
                    Success = false,
                    Message = "ID mismatch"
                });
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Dado Inválido");
                return BadRequest(new ServiceResponse<Character>
                {
                    Success = false,
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var response = await _characterService.UpdateCharacter(characterDto);

            if (!response.Success)
            {
                _logger.LogWarning("Ocorreu algum erro na atualização do Personagem");
                return NotFound(response);
            }

            _logger.LogInformation("Personagem {Id} atualizado com sucesso.", id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Iniciando a exclusão do Personagem {Id}", id);
            var response = await _characterService.DeleteCharacter(id);

            if (!response.Success)
            {
                _logger.LogWarning("Ocorreu algum erro na exclusão do Personagem");
                return NotFound(response);
            }

            _logger.LogInformation("Personagem {Id} excluido com sucesso.", id);
            return NoContent();
        }

        [HttpGet("comicvine/{comicVineId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<Character>>> GetByComicVineId(int comicVineId)
        {
            _logger.LogInformation("Iniciando a Busca do Personagem pelo VineId {ComicVineId}", comicVineId);
            var response = await _characterService.GetCharacterByComicVineId(comicVineId);
            _logger.LogInformation("Personagem {ComicVineId} localizado com sucesso.", comicVineId);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}