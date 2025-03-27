using ComicTracker.Application.DTOs;
using ComicTracker.Domain.Entities;

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

            // Filtrar publishers já existentes no banco
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
        }

        return response;
    }

    public async Task<ServiceResponse<Publisher>> CreatePublisher(PublisherCreateDto publisherDto)
    {
        var response = new ServiceResponse<Publisher>();

        try
        {
            // Verificar se já existe
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
                // Mapear outros campos...
            };

            await _publisherRepository.AddAsync(publisher);
            await _publisherRepository.SaveChangesAsync();

            response.Data = publisher;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    // Outros métodos...
}
