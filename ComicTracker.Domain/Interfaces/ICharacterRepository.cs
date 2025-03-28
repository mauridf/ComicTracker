using ComicTracker.Domain.Entities;

namespace ComicTracker.Domain.Interfaces;

public interface ICharacterRepository
{
    Task<bool> ExistsByComicVineId(int comicVineId);
    Task AddAsync(Character character);
    Task SaveChangesAsync();
    IQueryable<Character> GetAll();
    Task<Character> GetByIdAsync(int id);
    void Update(Character character);
    void Delete(Character character);
}
