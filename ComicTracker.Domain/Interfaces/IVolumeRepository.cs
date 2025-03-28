using ComicTracker.Domain.Entities;

namespace ComicTracker.Domain.Interfaces;

public interface IVolumeRepository
{
    Task<bool> ExistsByComicVineId(int comicVineId);
    Task AddAsync(Volume volume);
    Task SaveChangesAsync();
    IQueryable<Volume> GetAll();
    Task<Volume> GetByIdAsync(int id);
    void Update(Volume volume);
    void Delete(Volume volume);
    Task<Volume> GetByComicVineIdAsync(int comicVineId);
    Task<List<Volume>> GetVolumesByPublisherId(int publisherId);
}
