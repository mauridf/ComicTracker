using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ComicTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private readonly IPublisherService _publisherService;
    private readonly ILogger<PublishersController> _logger;

    public PublishersController(
        IPublisherService publisherService,
        ILogger<PublishersController> logger)
    {
        _publisherService = publisherService;
        _logger = logger;
    }

    /// <summary>
    /// Search publishers in Comic Vine API
    /// </summary>
    /// <param name="name">Publisher name to search</param>
    /// <returns>List of publishers from Comic Vine</returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<List<ComicVinePublisher>>>> Search(string name)
    {
        _logger.LogInformation("Iniciando busca por Editoras com o nome: {name} na API da Comic Vine", name);
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogWarning("Não foi informado um nome para efetuar a pesquia");
            return BadRequest(new ServiceResponse<List<ComicVinePublisher>>
            {
                Success = false,
                Message = "Name parameter is required"
            });
        }

        try
        {
            var response = await _publisherService.SearchPublishers(name);
            _logger.LogInformation("Editoras com o nome {name} encontradas com sucesso", name);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar editora com nome {name}", name);
            return StatusCode(500, new ServiceResponse<List<ComicVinePublisher>>
            {
                Success = false,
                Message = "Internal server error"
            });
        }
    }

    /// <summary>
    /// Create a new publisher
    /// </summary>
    /// <param name="publisherDto">Publisher data</param>
    /// <returns>Created publisher</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<Publisher>>> Create(PublisherCreateDto publisherDto)
    {
        _logger.LogInformation("Iniciando o registro de uma nova Editora");
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Dado inválido");
            return BadRequest(new ServiceResponse<Publisher>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        try
        {
            var response = await _publisherService.CreatePublisher(publisherDto);

            if (!response.Success)
            {
                _logger.LogWarning("Erro na gravação da Editora");
                return BadRequest(response);
            }

            _logger.LogInformation("Editora registrada com sucesso");
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no registro da Editora");
            return StatusCode(500, new ServiceResponse<Publisher>
            {
                Success = false,
                Message = "Internal server error"
            });
        }
    }

    /// <summary>
    /// Get publisher by ID
    /// </summary>
    /// <param name="id">Publisher ID</param>
    /// <returns>Publisher details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Publisher>>> GetById(int id)
    {
        _logger.LogInformation("Iniciando a busca da Editora com Id:{Id}", id);
        try
        {
            var response = await _publisherService.GetPublisherById(id);

            if (!response.Success)
            {
                _logger.LogWarning("Editora não localizada");
                return NotFound(response);
            }

            _logger.LogInformation("Editora id: {Id} localizada com sucesso", id);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao buscar Editora com o ID {id}");
            return StatusCode(500, new ServiceResponse<Publisher>
            {
                Success = false,
                Message = "Internal server error"
            });
        }
    }

    /// <summary>
    /// Get all publishers
    /// </summary>
    /// <returns>List of all publishers</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Publisher>>>> GetAll()
    {
        _logger.LogInformation("Iniciando a busca de Todas as Editoras cadastradas");
        try
        {
            var response = await _publisherService.GetAllPublishers();
            _logger.LogInformation("Todas as editoras cadastradas listadas com sucesso");
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar todas as editoras");
            return StatusCode(500, new ServiceResponse<List<Publisher>>
            {
                Success = false,
                Message = "Internal server error"
            });
        }
    }

    /// <summary>
    /// Update a publisher
    /// </summary>
    /// <param name="id">Publisher ID</param>
    /// <param name="publisherDto">Updated publisher data</param>
    /// <returns>Updated publisher</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Publisher>>> Update(int id, PublisherUpdateDto publisherDto)
    {
        _logger.LogInformation("Iniciando a atualização dos dados da Editora {Id}", id);
        if (id != publisherDto.Id)
        {
            _logger.LogWarning("Erro na atualização da Editora");
            return BadRequest(new ServiceResponse<Publisher>
            {
                Success = false,
                Message = "ID mismatch"
            });
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Dado Inválido");
            return BadRequest(new ServiceResponse<Publisher>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        try
        {
            var response = await _publisherService.UpdatePublisher(publisherDto);

            if (!response.Success)
            {
                _logger.LogWarning("Ocorreu algum erro na atualização da Editora");
                return NotFound(response);
            }

            _logger.LogInformation("Editora {Id} atualizada com sucesso.", id);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao atualizar Editora ID {id}");
            return StatusCode(500, new ServiceResponse<Publisher>
            {
                Success = false,
                Message = "Internal server error"
            });
        }
    }

    /// <summary>
    /// Delete a publisher
    /// </summary>
    /// <param name="id">Publisher ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Iniciando a exclusão da Editora {Id}", id);
        try
        {
            var response = await _publisherService.DeletePublisher(id);

            if (!response.Success)
            {
                _logger.LogWarning("Ocorreu algum erro na exclusão da Editora");
                return NotFound(response);
            }

            _logger.LogInformation("Editora {Id} excluida com sucesso.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao deletar Editora com ID {id}");
            return StatusCode(500, new ServiceResponse<Publisher>
            {
                Success = false,
                Message = "Internal server error"
            });
        }
    }
}