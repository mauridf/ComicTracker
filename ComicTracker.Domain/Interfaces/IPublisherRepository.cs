using ComicTracker.Domain.Entities;

namespace ComicTracker.Domain.Interfaces;

public interface IPublisherRepository
{
    Task<bool> ExistsByComicVineId(int comicVineId);
    Task AddAsync(Publisher publisher);
    Task SaveChangesAsync();
    IQueryable<Publisher> GetAll();
    Task<Publisher> GetByIdAsync(int id);
    void Update(Publisher publisher);
    void Delete(Publisher publisher);
}
