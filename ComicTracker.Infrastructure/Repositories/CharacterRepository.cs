using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using ComicTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Infrastructure.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly ComicTrackerDbContext _context;

    public CharacterRepository(ComicTrackerDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Character character)
    {
        await _context.Characters.AddAsync(character);
    }

    public async Task<bool> ExistsByComicVineId(int comicVineId)
    {
        return await _context.Characters.AnyAsync(c => c.ComicVineId == comicVineId);
    }

    public IQueryable<Character> GetAll()
    {
        return _context.Characters.AsQueryable();
    }

    public async Task<Character> GetByIdAsync(int id)
    {
        return await _context.Characters.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(Character character)
    {
        _context.Characters.Update(character);
    }

    public void Delete(Character character)
    {
        _context.Characters.Remove(character);
    }
}
