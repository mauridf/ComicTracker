using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ComicTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IIssueService _issueService;

    public IssuesController(IIssueService issueService)
    {
        _issueService = issueService;
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<List<ComicVineIssue>>>> SearchByVolumeComicVineId(int volumeComicVineId)
    {
        if (volumeComicVineId <= 0)
        {
            return BadRequest(new ServiceResponse<List<ComicVineIssue>>
            {
                Success = false,
                Message = "Volume ComicVine ID must be greater than 0"
            });
        }

        var response = await _issueService.SearchIssuesByVolume(volumeComicVineId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<Issue>>> Create(IssueCreateDto issueDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ServiceResponse<Issue>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _issueService.CreateIssue(issueDto);

        if (!response.Success)
            return BadRequest(response);

        return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Issue>>> GetById(int id)
    {
        var response = await _issueService.GetIssueById(id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("comicvine/{comicVineId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Issue>>> GetByComicVineId(int comicVineId)
    {
        var response = await _issueService.GetIssueByComicVineId(comicVineId);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Issue>>>> GetAll()
    {
        var response = await _issueService.GetAllIssues();
        return Ok(response);
    }

    [HttpGet("volume/{volumeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Issue>>>> GetByVolume(int volumeId)
    {
        var response = await _issueService.GetIssuesByVolume(volumeId);
        return Ok(response);
    }

    [HttpGet("read/{readStatus}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Issue>>>> GetByReadStatus(bool readStatus)
    {
        var response = await _issueService.GetIssuesByReadStatus(readStatus);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Issue>>> Update(int id, IssueUpdateDto issueDto)
    {
        if (id != issueDto.Id)
        {
            return BadRequest(new ServiceResponse<Issue>
            {
                Success = false,
                Message = "ID mismatch"
            });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new ServiceResponse<Issue>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _issueService.UpdateIssue(issueDto);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPatch("{id}/read")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Issue>>> MarkAsRead(int id, [FromBody] bool readStatus)
    {
        var response = await _issueService.MarkAsRead(id, readStatus);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _issueService.DeleteIssue(id);

        if (!response.Success)
            return NotFound(response);

        return NoContent();
    }
}