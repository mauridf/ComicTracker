using ComicTracker.Application.DTOs;
using ComicTracker.Domain.Entities;

namespace ComicTracker.Application.Interfaces;

public interface IIssueService
{
    Task<ServiceResponse<List<ComicVineIssue>>> SearchIssuesByVolume(int volumeComicVineId);
    Task<ServiceResponse<Issue>> CreateIssue(IssueCreateDto issueDto);
    Task<ServiceResponse<Issue>> GetIssueById(int id);
    Task<ServiceResponse<Issue>> GetIssueByComicVineId(int comicVineId);
    Task<ServiceResponse<List<Issue>>> GetAllIssues();
    Task<ServiceResponse<List<Issue>>> GetIssuesByVolume(int volumeId);
    Task<ServiceResponse<List<Issue>>> GetIssuesByReadStatus(bool readStatus);
    Task<ServiceResponse<Issue>> UpdateIssue(IssueUpdateDto issueDto);
    Task<ServiceResponse<Issue>> MarkAsRead(int id, bool readStatus);
    Task<ServiceResponse<bool>> DeleteIssue(int id);
}