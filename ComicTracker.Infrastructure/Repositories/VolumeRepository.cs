using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using ComicTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Infrastructure.Repositories;

public class VolumeRepository : IVolumeRepository
{
    private readonly ComicTrackerDbContext _context;

    public VolumeRepository(ComicTrackerDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Volume volume)
    {
        await _context.Volumes.AddAsync(volume);
    }

    public async Task<bool> ExistsByComicVineId(int comicVineId)
    {
        return await _context.Volumes.AnyAsync(v => v.ComicVineId == comicVineId);
    }

    public IQueryable<Volume> GetAll()
    {
        return _context.Volumes
            .Include(v => v.Publisher)
            .Include(v => v.Issues)
            .AsQueryable();
    }

    public async Task<Volume> GetByIdAsync(int id)
    {
        return await _context.Volumes
            .Include(v => v.Publisher)
            .Include(v => v.Issues)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Volume> GetByComicVineIdAsync(int comicVineId)
    {
        return await _context.Volumes
            .Include(v => v.Publisher)
            .FirstOrDefaultAsync(v => v.ComicVineId == comicVineId);
    }

    public async Task<List<Volume>> GetVolumesByPublisherId(int publisherId)
    {
        return await _context.Volumes
            .Where(v => v.PublisherId == publisherId)
            .Include(v => v.Publisher)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(Volume volume)
    {
        _context.Volumes.Update(volume);
    }

    public void Delete(Volume volume)
    {
        _context.Volumes.Remove(volume);
    }
}