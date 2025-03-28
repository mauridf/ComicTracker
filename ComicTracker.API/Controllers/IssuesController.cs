using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ComicTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly ILogger<PublishersController> _logger;
    private readonly IIssueService _issueService;

    public IssuesController(ILogger<PublishersController> logger, IIssueService issueService)
    {
        _logger = logger;
        _issueService = issueService;
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<List<ComicVineIssue>>>> SearchByVolumeComicVineId(int volumeComicVineId)
    {
        _logger.LogInformation("Iniciando busca por Edições do Volume: {VolumeComicVineId} na API da Comic Vine", volumeComicVineId);
        if (volumeComicVineId <= 0)
        {
            _logger.LogWarning("O valor do Id informado é nulo ou 0");
            return BadRequest(new ServiceResponse<List<ComicVineIssue>>
            {
                Success = false,
                Message = "Volume ComicVine ID must be greater than 0"
            });
        }

        var response = await _issueService.SearchIssuesByVolume(volumeComicVineId);
        _logger.LogInformation("Edições do volume {VolumeComicVineId} encontradas com sucesso", volumeComicVineId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<Issue>>> Create(IssueCreateDto issueDto)
    {
        _logger.LogInformation("Iniciando o registro de uma nova Edição");
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Dado inválido");
            return BadRequest(new ServiceResponse<Issue>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _issueService.CreateIssue(issueDto);

        if (!response.Success)
        {
            _logger.LogWarning("Erro na gravação da Edição");
            return BadRequest(response);
        }

        return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Issue>>> GetById(int id)
    {
        _logger.LogInformation("Iniciando a busca da Edição com Id:{Id}", id);
        var response = await _issueService.GetIssueById(id);
        _logger.LogInformation("Edição id: {Id} localizada com sucesso", id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("comicvine/{comicVineId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Issue>>> GetByComicVineId(int comicVineId)
    {
        _logger.LogInformation("Iniciando a busca da Edição com ComicVineId:{ComicVineId}", comicVineId);
        var response = await _issueService.GetIssueByComicVineId(comicVineId);
        _logger.LogInformation("Edição ComicVineId: {comicVineId} localizada com sucesso", comicVineId);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Issue>>>> GetAll()
    {
        _logger.LogInformation("Iniciando a busca de Todas as Edições cadastradas");
        var response = await _issueService.GetAllIssues();
        _logger.LogInformation("Todos as edições cadastradas listadas com sucesso");
        return Ok(response);
    }

    [HttpGet("volume/{volumeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Issue>>>> GetByVolume(int volumeId)
    {
        _logger.LogInformation("Iniciando a busca da Edição com VolumeId:{VolumeId}", volumeId);
        var response = await _issueService.GetIssuesByVolume(volumeId);
        _logger.LogInformation("Edição VolumeId: {volumeId} localizada com sucesso", volumeId);
        return Ok(response);
    }

    [HttpGet("read/{readStatus}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Issue>>>> GetByReadStatus(bool readStatus)
    {
        _logger.LogInformation("Iniciando a busca das Edição com status de Lidas ou Não Lidas");
        var response = await _issueService.GetIssuesByReadStatus(readStatus);
        _logger.LogInformation("Edições Lidas ou Não Lidas listadas com sucesso");
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Issue>>> Update(int id, IssueUpdateDto issueDto)
    {
        _logger.LogInformation("Iniciando a atualização dos dados da Edição {Id}", id);
        if (id != issueDto.Id)
        {
            _logger.LogWarning("Erro na atualização da edição");
            return BadRequest(new ServiceResponse<Issue>
            {
                Success = false,
                Message = "ID mismatch"
            });
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Dado Inválido");
            return BadRequest(new ServiceResponse<Issue>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _issueService.UpdateIssue(issueDto);

        if (!response.Success)
        {
            _logger.LogWarning("Ocorreu algum erro na atualização da Edição");
            return NotFound(response);
        }

        _logger.LogInformation("Edição {Id} atualizada com sucesso.", id);
        return Ok(response);
    }

    [HttpPatch("{id}/read")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Issue>>> MarkAsRead(int id, [FromBody] bool readStatus)
    {
        _logger.LogInformation("Iniciando a marcação da Edição {Id} como lida", id);
        var response = await _issueService.MarkAsRead(id, readStatus);
        _logger.LogInformation("Edição {Id} marcada como lida", id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Iniciando a exclusão da Edição {Id}", id);
        var response = await _issueService.DeleteIssue(id);

        if (!response.Success)
        {
            _logger.LogWarning("Ocorreu algum erro na exclusão da Edição");
            return NotFound(response);
        }

        _logger.LogInformation("Edição {Id} excluida com sucesso.", id);
        return NoContent();
    }
}