using ComicTracker.Domain.Entities;
using ComicTracker.Infrastructure.Data;

namespace ComicTracker.Infrastructure.Repositories;

public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
{
    public PublisherRepository(ComicTrackerDbContext context) : base(context) { }

    public async Task<bool> ExistsByComicVineId(int comicVineId)
    {
        return await _dbSet.AnyAsync(p => p.ComicVineId == comicVineId);
    }

    // Outros métodos específicos para Publisher
}