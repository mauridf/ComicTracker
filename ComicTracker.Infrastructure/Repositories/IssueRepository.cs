using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using ComicTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Infrastructure.Repositories;

public class IssueRepository : IIssueRepository
{
    private readonly ComicTrackerDbContext _context;

    public IssueRepository(ComicTrackerDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Issue issue)
    {
        await _context.Issues.AddAsync(issue);
    }

    public async Task<bool> ExistsByComicVineId(int comicVineId)
    {
        return await _context.Issues.AnyAsync(i => i.ComicVineId == comicVineId);
    }

    public IQueryable<Issue> GetAll()
    {
        return _context.Issues
            .Include(i => i.Volume)
            .ThenInclude(v => v.Publisher)
            .AsQueryable();
    }

    public async Task<Issue> GetByIdAsync(int id)
    {
        return await _context.Issues
            .Include(i => i.Volume)
            .ThenInclude(v => v.Publisher)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Issue> GetByComicVineIdAsync(int comicVineId)
    {
        return await _context.Issues
            .Include(i => i.Volume)
            .FirstOrDefaultAsync(i => i.ComicVineId == comicVineId);
    }

    public async Task<List<Issue>> GetIssuesByVolumeId(int volumeId)
    {
        return await _context.Issues
            .Where(i => i.VolumeId == volumeId)
            .Include(i => i.Volume) 
            .ToListAsync();
    }

    public async Task<List<Issue>> GetReadIssues(bool readStatus)
    {
        return await _context.Issues
            .Where(i => i.Read == readStatus)
            .Include(i => i.Volume)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(Issue issue)
    {
        _context.Issues.Update(issue);
    }

    public void Delete(Issue issue)
    {
        _context.Issues.Remove(issue);
    }
}