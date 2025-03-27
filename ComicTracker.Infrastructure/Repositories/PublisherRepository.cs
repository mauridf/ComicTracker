using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using ComicTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Infrastructure.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly ComicTrackerDbContext _context;

    public PublisherRepository(ComicTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByComicVineId(int comicVineId)
    {
        return await _context.Publishers.AnyAsync(p => p.ComicVineId == comicVineId);
    }

    public async Task AddAsync(Publisher publisher)
    {
        await _context.Publishers.AddAsync(publisher);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IQueryable<Publisher> GetAll()
    {
        return _context.Publishers.AsQueryable();
    }

    public async Task<Publisher> GetByIdAsync(int id)
    {
        return await _context.Publishers.FindAsync(id);
    }
}