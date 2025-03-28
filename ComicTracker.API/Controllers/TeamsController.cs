using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ComicTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ILogger<IssuesController> _logger;
    private readonly ITeamService _teamService;

    public TeamsController(ILogger<IssuesController> logger, ITeamService teamService)
    {
        _logger = logger;
        _teamService = teamService;
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<List<ComicVineTeam>>>> Search(string name)
    {
        _logger.LogInformation("Iniciando busca por Equipes com o nome: {name} na API da Comic Vine", name);
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogWarning("Não foi informado um nome para efetuar a pesquia");
            return BadRequest(new ServiceResponse<List<ComicVineTeam>>
            {
                Success = false,
                Message = "Name parameter is required"
            });
        }

        var response = await _teamService.SearchTeams(name);
        _logger.LogInformation("Equipes com o nome {name} encontradas com sucesso", name);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<Team>>> Create(TeamCreateDto teamDto)
    {
        _logger.LogInformation("Iniciando o registro de uma nova Equipe");
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Dado inválido");
            return BadRequest(new ServiceResponse<Team>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _teamService.CreateTeam(teamDto);

        if (!response.Success)
        {
            _logger.LogWarning("Erro na gravação da Equipe");
            return BadRequest(response);
        }

        _logger.LogInformation("Equipe registrada com sucesso");
        return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Team>>> GetById(int id)
    {
        _logger.LogInformation("Iniciando a busca da Equipe com Id:{Id}", id);
        var response = await _teamService.GetTeamById(id);
        _logger.LogInformation("Equipe id: {Id} localizada com sucesso", id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("comicvine/{comicVineId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Team>>> GetByComicVineId(int comicVineId)
    {
        _logger.LogInformation("Iniciando a busca da Equipe pelo VineId:{VineId}", comicVineId);
        var response = await _teamService.GetTeamByComicVineId(comicVineId);
        _logger.LogInformation("Equipe VineId: {VineId} localizada com sucesso", comicVineId);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Team>>>> GetAll()
    {
        _logger.LogInformation("Iniciando a busca de Todas as Equipes cadastradas");
        var response = await _teamService.GetAllTeams();
        _logger.LogInformation("Todas as equipes cadastradas listadas com sucesso");
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Team>>> Update(int id, TeamUpdateDto teamDto)
    {
        _logger.LogInformation("Iniciando a atualização dos dados da Equipe {Id}", id);
        if (id != teamDto.Id)
        {
            _logger.LogWarning("Erro na atualização da Equipe");
            return BadRequest(new ServiceResponse<Team>
            {
                Success = false,
                Message = "ID mismatch"
            });
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Dado Inválido");
            return BadRequest(new ServiceResponse<Team>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _teamService.UpdateTeam(teamDto);

        if (!response.Success)
        {
            _logger.LogWarning("Ocorreu algum erro na atualização da Equipe");
            return NotFound(response);
        }

        _logger.LogInformation("Equipe {Id} atualizada com sucesso.", id);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Iniciando a exclusão da Equipe {Id}", id);
        var response = await _teamService.DeleteTeam(id);

        if (!response.Success)
        {
            _logger.LogWarning("Ocorreu algum erro na exclusão da Equipe");
            return NotFound(response);
        }

        _logger.LogInformation("Equipe {Id} excluida com sucesso.", id);
        return NoContent();
    }
}