using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ComicTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VolumesController : ControllerBase
{
    private readonly IVolumeService _volumeService;

    public VolumesController(IVolumeService volumeService)
    {
        _volumeService = volumeService;
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<List<ComicVineVolume>>>> Search(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest(new ServiceResponse<List<ComicVineVolume>>
            {
                Success = false,
                Message = "Name parameter is required"
            });
        }

        var response = await _volumeService.SearchVolumes(name);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<Volume>>> Create(VolumeCreateDto volumeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ServiceResponse<Volume>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _volumeService.CreateVolume(volumeDto);

        if (!response.Success)
            return BadRequest(response);

        return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Volume>>> GetById(int id)
    {
        var response = await _volumeService.GetVolumeById(id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("comicvine/{comicVineId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Volume>>> GetByComicVineId(int comicVineId)
    {
        var response = await _volumeService.GetVolumeByComicVineId(comicVineId);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Volume>>>> GetAll()
    {
        var response = await _volumeService.GetAllVolumes();
        return Ok(response);
    }

    [HttpGet("publisher/{publisherId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Volume>>>> GetByPublisher(int publisherId)
    {
        var response = await _volumeService.GetVolumesByPublisher(publisherId);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Volume>>> Update(int id, VolumeUpdateDto volumeDto)
    {
        if (id != volumeDto.Id)
        {
            return BadRequest(new ServiceResponse<Volume>
            {
                Success = false,
                Message = "ID mismatch"
            });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new ServiceResponse<Volume>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _volumeService.UpdateVolume(volumeDto);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _volumeService.DeleteVolume(id);

        if (!response.Success)
            return NotFound(response);

        return NoContent();
    }
}