using ComicTracker.Domain.Entities;

namespace ComicTracker.Domain.Interfaces;

public interface IPublisherRepository
{
    Task<bool> ExistsByComicVineId(int comicVineId);
    Task AddAsync(Publisher publisher);
    Task SaveChangesAsync();
    IQueryable<Publisher> GetAll();
    // Adicione outros métodos conforme necessário
}
