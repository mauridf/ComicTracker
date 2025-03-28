using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IComicVineService _comicVineService;

    public TeamService(
        ITeamRepository teamRepository,
        IComicVineService comicVineService)
    {
        _teamRepository = teamRepository;
        _comicVineService = comicVineService;
    }

    public async Task<ServiceResponse<List<ComicVineTeam>>> SearchTeams(string name)
    {
        var response = new ServiceResponse<List<ComicVineTeam>>();

        try
        {
            var comicVineResponse = await _comicVineService.GetTeams($"name:{name}");

            if (comicVineResponse.Error != "OK")
            {
                response.Success = false;
                response.Message = "Error from Comic Vine API";
                return response;
            }

            var existingIds = await _teamRepository.GetAll()
                .Where(t => comicVineResponse.Results.Select(cv => cv.Id).Contains(t.ComicVineId))
                .Select(t => t.ComicVineId)
                .ToListAsync();

            var filteredResults = comicVineResponse.Results
                .Where(r => !existingIds.Contains(r.Id))
                .ToList();

            response.Data = filteredResults;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    public async Task<ServiceResponse<Team>> CreateTeam(TeamCreateDto teamDto)
    {
        var response = new ServiceResponse<Team>();

        try
        {
            if (await _teamRepository.ExistsByComicVineId(teamDto.ComicVineId))
            {
                response.Success = false;
                response.Message = "Team already exists";
                return response;
            }

            var team = new Team
            {
                ComicVineId = teamDto.ComicVineId,
                Name = teamDto.Name,
                Aliases = teamDto.Aliases,
                CountOfIssueAppearances = teamDto.CountOfIssueAppearances,
                CountOfTeamMembers = teamDto.CountOfTeamMembers,
                Deck = teamDto.Deck,
                Description = teamDto.Description,
                FirstAppearedInIssue = teamDto.FirstAppearedInIssue,
                ImageUrl = teamDto.ImageUrl,
                PublisherName = teamDto.PublisherName,
                SiteDetailUrl = teamDto.SiteDetailUrl
            };

            await _teamRepository.AddAsync(team);
            await _teamRepository.SaveChangesAsync();

            response.Data = team;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    public async Task<ServiceResponse<Team>> GetTeamById(int id)
    {
        var response = new ServiceResponse<Team>();
        try
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null)
            {
                response.Success = false;
                response.Message = "Team not found";
                return response;
            }
            response.Data = team;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<Team>> GetTeamByComicVineId(int comicVineId)
    {
        var response = new ServiceResponse<Team>();
        try
        {
            var team = await _teamRepository.GetByComicVineIdAsync(comicVineId);
            if (team == null)
            {
                response.Success = false;
                response.Message = "Team not found";
                return response;
            }
            response.Data = team;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<List<Team>>> GetAllTeams()
    {
        var response = new ServiceResponse<List<Team>>();
        try
        {
            var teams = await _teamRepository.GetAll().ToListAsync();
            response.Data = teams;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<Team>> UpdateTeam(TeamUpdateDto teamDto)
    {
        var response = new ServiceResponse<Team>();
        try
        {
            var team = await _teamRepository.GetByIdAsync(teamDto.Id);
            if (team == null)
            {
                response.Success = false;
                response.Message = "Team not found";
                return response;
            }

            // Atualiza os campos
            team.Name = teamDto.Name;
            team.Aliases = teamDto.Aliases;
            team.CountOfIssueAppearances = teamDto.CountOfIssueAppearances;
            team.CountOfTeamMembers = teamDto.CountOfTeamMembers;
            team.Deck = teamDto.Deck;
            team.Description = teamDto.Description;
            team.FirstAppearedInIssue = teamDto.FirstAppearedInIssue;
            team.ImageUrl = teamDto.ImageUrl;
            team.PublisherName = teamDto.PublisherName;
            team.SiteDetailUrl = teamDto.SiteDetailUrl;

            _teamRepository.Update(team);
            await _teamRepository.SaveChangesAsync();

            response.Data = team;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<bool>> DeleteTeam(int id)
    {
        var response = new ServiceResponse<bool>();
        try
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null)
            {
                response.Success = false;
                response.Message = "Team not found";
                return response;
            }

            _teamRepository.Delete(team);
            await _teamRepository.SaveChangesAsync();

            response.Data = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
            response.Data = false;
        }
        return response;
    }
}