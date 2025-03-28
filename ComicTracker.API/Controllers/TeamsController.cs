using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ComicTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<List<ComicVineTeam>>>> Search(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest(new ServiceResponse<List<ComicVineTeam>>
            {
                Success = false,
                Message = "Name parameter is required"
            });
        }

        var response = await _teamService.SearchTeams(name);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<Team>>> Create(TeamCreateDto teamDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ServiceResponse<Team>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _teamService.CreateTeam(teamDto);

        if (!response.Success)
            return BadRequest(response);

        return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Team>>> GetById(int id)
    {
        var response = await _teamService.GetTeamById(id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("comicvine/{comicVineId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Team>>> GetByComicVineId(int comicVineId)
    {
        var response = await _teamService.GetTeamByComicVineId(comicVineId);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Team>>>> GetAll()
    {
        var response = await _teamService.GetAllTeams();
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Team>>> Update(int id, TeamUpdateDto teamDto)
    {
        if (id != teamDto.Id)
        {
            return BadRequest(new ServiceResponse<Team>
            {
                Success = false,
                Message = "ID mismatch"
            });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new ServiceResponse<Team>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _teamService.UpdateTeam(teamDto);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _teamService.DeleteTeam(id);

        if (!response.Success)
            return NotFound(response);

        return NoContent();
    }
}