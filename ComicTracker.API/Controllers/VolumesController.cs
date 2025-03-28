using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ComicTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VolumesController : ControllerBase
{
    private readonly ILogger<IssuesController> _logger;
    private readonly IVolumeService _volumeService;

    public VolumesController(ILogger<IssuesController> logger, IVolumeService volumeService)
    {
        _logger = logger;
        _volumeService = volumeService;
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<List<ComicVineVolume>>>> Search(string name)
    {
        _logger.LogInformation("Iniciando busca por Volume com o nome: {name} na API da Comic Vine", name);
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogWarning("Não foi informado um nome para efetuar a pesquia");
            return BadRequest(new ServiceResponse<List<ComicVineVolume>>
            {
                Success = false,
                Message = "Name parameter is required"
            });
        }

        var response = await _volumeService.SearchVolumes(name);
        _logger.LogInformation("Volumes com o nome {name} encontrados com sucesso", name);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<Volume>>> Create(VolumeCreateDto volumeDto)
    {
        _logger.LogInformation("Iniciando o registro de um novo Volume");
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Dado inválido");
            return BadRequest(new ServiceResponse<Volume>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _volumeService.CreateVolume(volumeDto);

        if (!response.Success)
        {
            _logger.LogWarning("Erro na gravação do Volume");
            return BadRequest(response);
        }

        _logger.LogInformation("Volume registrado com sucesso");
        return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Volume>>> GetById(int id)
    {
        _logger.LogInformation("Iniciando a busca do Volume com Id:{Id}",id);
        var response = await _volumeService.GetVolumeById(id);
        _logger.LogInformation("Volume id: {Id} localizado com sucesso", id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("comicvine/{comicVineId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Volume>>> GetByComicVineId(int comicVineId)
    {
        _logger.LogInformation("Iniciando a busca do Volume pelo VineId:{VineId}", comicVineId);
        var response = await _volumeService.GetVolumeByComicVineId(comicVineId);
        _logger.LogInformation("Volume VineId: {VineId} localizado com sucesso", comicVineId);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Volume>>>> GetAll()
    {
        _logger.LogInformation("Iniciando a busca de Todos os Volumes cadastrados");
        var response = await _volumeService.GetAllVolumes();
        _logger.LogInformation("Todos os volumes cadastrados listados com sucesso");
        return Ok(response);
    }

    [HttpGet("publisher/{publisherId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ServiceResponse<List<Volume>>>> GetByPublisher(int publisherId)
    {
        _logger.LogInformation("Iniciando a busca de Todos os Volumes da EditoraId: {PublisherId}",publisherId);
        var response = await _volumeService.GetVolumesByPublisher(publisherId);
        _logger.LogInformation("Todos os volumes da EditoraId: {PublisherId}",publisherId);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceResponse<Volume>>> Update(int id, VolumeUpdateDto volumeDto)
    {
        _logger.LogInformation("Iniciando a atualização dos dados do Volume {Id}", id);
        if (id != volumeDto.Id)
        {
            _logger.LogWarning("Erro na atualização do Volume");
            return BadRequest(new ServiceResponse<Volume>
            {
                Success = false,
                Message = "ID mismatch"
            });
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Dado Inválido");
            return BadRequest(new ServiceResponse<Volume>
            {
                Success = false,
                Message = "Invalid data",
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var response = await _volumeService.UpdateVolume(volumeDto);

        if (!response.Success)
        {
            _logger.LogWarning("Ocorreu algum erro na atualização do Volume");
            return NotFound(response);
        }

        _logger.LogInformation("Volume {Id} atualizado com sucesso.", id);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Iniciando a exclusão do Volume {Id}", id);
        var response = await _volumeService.DeleteVolume(id);

        if (!response.Success)
        {
            _logger.LogWarning("Ocorreu algum erro na exclusão do Volume");
            return NotFound(response);
        }

        _logger.LogInformation("Volume {Id} excluido com sucesso.", id);
        return NoContent();
    }
}