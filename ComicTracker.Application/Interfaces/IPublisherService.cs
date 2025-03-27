using ComicTracker.Application.DTOs;
using ComicTracker.Domain.Entities;

namespace ComicTracker.Application.Interfaces;

public interface IPublisherService
{
    Task<ServiceResponse<List<ComicVinePublisher>>> SearchPublishers(string name);
    Task<ServiceResponse<Publisher>> GetPublisherById(int id);
    Task<ServiceResponse<List<Publisher>>> GetAllPublishers();
    Task<ServiceResponse<Publisher>> CreatePublisher(PublisherCreateDto publisherDto);
    Task<ServiceResponse<Publisher>> UpdatePublisher(PublisherUpdateDto publisherDto);
    Task<ServiceResponse<bool>> DeletePublisher(int id);
}
