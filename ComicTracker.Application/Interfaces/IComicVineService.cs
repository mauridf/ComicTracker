using ComicTracker.Application.DTOs;
using ComicTracker.Application.DTOs.ComicVine;

namespace ComicTracker.Application.Interfaces;

public interface IComicVineService
{
    Task<ComicVineResponse<ComicVinePublisher>> GetPublishers(string filter);
    Task<ComicVineResponse<ComicVineCharacter>> GetCharacters(string filter);
    Task<ComicVineResponse<ComicVineTeam>> GetTeams(string filter);
    Task<ComicVineResponse<ComicVineVolume>> GetVolumes(string filter);
    Task<ComicVineResponse<ComicVineIssue>> GetIssues(string filter);
    Task<ComicVineResponse<ComicVineIssue>> GetIssuesByVolumeId(int volumeId);
    Task<ComicVineResponse<ComicVineIssue>> GetIssuesByVolume(int volumeId);
}