using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;

namespace ComicTracker.Application.Services;

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IComicVineService _comicVineService;

    public PublisherService(
        IPublisherRepository publisherRepository,
        IComicVineService comicVineService)
    {
        _publisherRepository = publisherRepository;
        _comicVineService = comicVineService;
    }

    public async Task<ServiceResponse<List<ComicVinePublisher>>> SearchPublishers(string name)
    {
        var response = new ServiceResponse<List<ComicVinePublisher>>();

        try
        {
            var comicVineResponse = await _comicVineService.GetPublishers($"name:{name}");

            if (comicVineResponse.Error != "OK")
            {
                response.Success = false;
                response.Message = "Error from Comic Vine API";
                return response;
            }

            var existingIds = await _publisherRepository.GetAll()
                .Where(p => comicVineResponse.Results.Select(cv => cv.Id).Contains(p.ComicVineId))
                .Select(p => p.ComicVineId)
                .ToListAsync();

            var filteredResults = comicVineResponse.Results
                .Where(r => !existingIds.Contains(r.Id))
                .ToList();

            response.Data = filteredResults;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    public async Task<ServiceResponse<Publisher>> CreatePublisher(PublisherCreateDto publisherDto)
    {
        var response = new ServiceResponse<Publisher>();

        try
        {
            if (await _publisherRepository.ExistsByComicVineId(publisherDto.ComicVineId))
            {
                response.Success = false;
                response.Message = "Publisher already exists";
                return response;
            }

            var publisher = new Publisher
            {
                ComicVineId = publisherDto.ComicVineId,
                Name = publisherDto.Name,
                Aliases = publisherDto.Aliases,
                Deck = publisherDto.Deck,
                ImageUrl = publisherDto.ImageUrl,
                LocationAddress = publisherDto.LocationAddress,
                LocationCity = publisherDto.LocationCity,
                LocationState = publisherDto.LocationState,
                SiteDetailUrl = publisherDto.SiteDetailUrl
            };

            await _publisherRepository.AddAsync(publisher);
            await _publisherRepository.SaveChangesAsync();

            response.Data = publisher;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    // Implementação dos outros métodos
    public async Task<ServiceResponse<Publisher>> GetPublisherById(int id)
    {
        var response = new ServiceResponse<Publisher>();
        try
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher == null)
            {
                response.Success = false;
                response.Message = "Publisher not found";
                return response;
            }
            response.Data = publisher;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<List<Publisher>>> GetAllPublishers()
    {
        var response = new ServiceResponse<List<Publisher>>();
        try
        {
            var publishers = await _publisherRepository.GetAll().ToListAsync();
            response.Data = publishers;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }
}