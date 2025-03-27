using ComicTracker.Application.DTOs;
using ComicTracker.Application.Services;
using ComicTracker.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComicTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<List<ComicVinePublisher>>>> Search(string name)
        {
            var response = await _publisherService.SearchPublishers(name);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Publisher>>> Create(PublisherCreateDto publisherDto)
        {
            var response = await _publisherService.CreatePublisher(publisherDto);

            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(Get), response);
        }

        // Outros endpoints...
    }
}
