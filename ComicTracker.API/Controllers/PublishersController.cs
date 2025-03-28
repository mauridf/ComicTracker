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
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest(new ServiceResponse<List<ComicVinePublisher>>
            {
                Success = false,
                Message = "Name parameter is required"
            });
        }

        try
        {
            var response = await _publisherService.SearchPublishers(name);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching publishers");
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
        if (!ModelState.IsValid)
        {
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
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating publisher");
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
        try
        {
            var response = await _publisherService.GetPublisherById(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting publisher with ID {id}");
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
        try
        {
            var response = await _publisherService.GetAllPublishers();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all publishers");
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
        if (id != publisherDto.Id)
        {
            return BadRequest(new ServiceResponse<Publisher>
            {
                Success = false,
                Message = "ID mismatch"
            });
        }

        if (!ModelState.IsValid)
        {
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
                return NotFound(response);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating publisher with ID {id}");
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
        try
        {
            var response = await _publisherService.DeletePublisher(id);

            if (!response.Success)
                return NotFound(response);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting publisher with ID {id}");
            return StatusCode(500, new ServiceResponse<Publisher>
            {
                Success = false,
                Message = "Internal server error"
            });
        }
    }
}