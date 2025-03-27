using ComicTracker.Application.DTOs;

namespace ComicTracker.Application.Interfaces;

public interface IComicVineService
{
    Task<ComicVineResponse<ComicVinePublisher>> GetPublishers(string filter);
    // Adicione outros métodos para Characters, Teams, etc.
}
