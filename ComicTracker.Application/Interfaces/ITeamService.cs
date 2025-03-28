using ComicTracker.Application.DTOs;
using ComicTracker.Domain.Entities;

namespace ComicTracker.Application.Interfaces;

public interface ITeamService
{
    Task<ServiceResponse<List<ComicVineTeam>>> SearchTeams(string name);
    Task<ServiceResponse<Team>> CreateTeam(TeamCreateDto teamDto);
    Task<ServiceResponse<Team>> GetTeamById(int id);
    Task<ServiceResponse<Team>> GetTeamByComicVineId(int comicVineId);
    Task<ServiceResponse<List<Team>>> GetAllTeams();
    Task<ServiceResponse<Team>> UpdateTeam(TeamUpdateDto teamDto);
    Task<ServiceResponse<bool>> DeleteTeam(int id);
}