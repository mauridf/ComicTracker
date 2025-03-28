using ComicTracker.Domain.Entities;

namespace ComicTracker.Domain.Interfaces;

public interface IIssueRepository
{
    Task<bool> ExistsByComicVineId(int comicVineId);
    Task AddAsync(Issue issue);
    Task SaveChangesAsync();
    IQueryable<Issue> GetAll();
    Task<Issue> GetByIdAsync(int id);
    void Update(Issue issue);
    void Delete(Issue issue);
    Task<Issue> GetByComicVineIdAsync(int comicVineId);
    Task<List<Issue>> GetIssuesByVolumeId(int volumeId);
    Task<List<Issue>> GetReadIssues(bool readStatus);
}