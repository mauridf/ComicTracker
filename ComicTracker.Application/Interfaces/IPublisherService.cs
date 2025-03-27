using ComicTracker.Application.DTOs;
using ComicTracker.Domain.Entities;

namespace ComicTracker.Application.Interfaces;

public interface IPublisherService
{
    Task<ServiceResponse<List<ComicVinePublisher>>> SearchPublishers(string name);
    Task<ServiceResponse<Publisher>> CreatePublisher(PublisherCreateDto publisherDto);
    // Adicione outros métodos conforme necessário
}
