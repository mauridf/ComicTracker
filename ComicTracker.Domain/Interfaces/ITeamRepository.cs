using ComicTracker.Domain.Entities;

namespace ComicTracker.Domain.Interfaces;

public interface ITeamRepository
{
    Task<bool> ExistsByComicVineId(int comicVineId);
    Task AddAsync(Team team);
    Task SaveChangesAsync();
    IQueryable<Team> GetAll();
    Task<Team> GetByIdAsync(int id);
    void Update(Team team);
    void Delete(Team team);
    Task<Team> GetByComicVineIdAsync(int comicVineId);
}