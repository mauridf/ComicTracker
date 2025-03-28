using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using ComicTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly ComicTrackerDbContext _context;

    public TeamRepository(ComicTrackerDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Team team)
    {
        await _context.Teams.AddAsync(team);
    }

    public async Task<bool> ExistsByComicVineId(int comicVineId)
    {
        return await _context.Teams.AnyAsync(t => t.ComicVineId == comicVineId);
    }

    public IQueryable<Team> GetAll()
    {
        return _context.Teams.AsQueryable();
    }

    public async Task<Team> GetByIdAsync(int id)
    {
        return await _context.Teams.FindAsync(id);
    }

    public async Task<Team> GetByComicVineIdAsync(int comicVineId)
    {
        return await _context.Teams
            .FirstOrDefaultAsync(t => t.ComicVineId == comicVineId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(Team team)
    {
        _context.Teams.Update(team);
    }

    public void Delete(Team team)
    {
        _context.Teams.Remove(team);
    }
}
