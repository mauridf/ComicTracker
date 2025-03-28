using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Application.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IComicVineService _comicVineService;
        private readonly IVolumeRepository _volumeRepository;

        public IssueService(
            IIssueRepository issueRepository,
            IComicVineService comicVineService,
            IVolumeRepository volumeRepository)
        {
            _issueRepository = issueRepository;
            _comicVineService = comicVineService;
            _volumeRepository = volumeRepository;
        }

        public async Task<ServiceResponse<List<ComicVineIssue>>> SearchIssuesByVolume(int volumeComicVineId)
        {
            var response = new ServiceResponse<List<ComicVineIssue>>();

            try
            {
                // Busca as issues na API do ComicVine filtradas pelo volumeId
                var comicVineResponse = await _comicVineService.GetIssuesByVolume(volumeComicVineId);

                if (comicVineResponse.Error != "OK")
                {
                    response.Success = false;
                    response.Message = "Error from Comic Vine API: " + comicVineResponse.Error;
                    return response;
                }

                // Filtra apenas as issues que ainda não existem no banco
                var existingIds = await _issueRepository.GetAll()
                    .Where(i => comicVineResponse.Results.Select(cv => cv.Id).Contains(i.ComicVineId))
                    .Select(i => i.ComicVineId)
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

        public async Task<ServiceResponse<Issue>> CreateIssue(IssueCreateDto issueDto)
        {
            var response = new ServiceResponse<Issue>();

            try
            {
                if (await _issueRepository.ExistsByComicVineId(issueDto.ComicVineId))
                {
                    response.Success = false;
                    response.Message = "Issue already exists";
                    return response;
                }

                var volume = await _volumeRepository.GetByIdAsync(issueDto.VolumeId);
                if (volume == null)
                {
                    response.Success = false;
                    response.Message = "Volume not found";
                    return response;
                }

                var issue = new Issue
                {
                    ComicVineId = issueDto.ComicVineId,
                    VolumeId = issueDto.VolumeId,
                    Aliases = issueDto.Aliases,
                    CoverDate = issueDto.CoverDate,
                    Deck = issueDto.Deck,
                    Description = issueDto.Description,
                    HasStaffReview = issueDto.HasStaffReview,
                    ImageUrl = issueDto.ImageUrl,
                    IssueNumber = issueDto.IssueNumber,
                    Name = issueDto.Name,
                    SiteDetailUrl = issueDto.SiteDetailUrl,
                    StoreDate = issueDto.StoreDate,
                    VolumeDetails = issueDto.VolumeDetails,
                    Read = issueDto.Read,
                    Notes = issueDto.Notes,
                    Rating = issueDto.Rating
                };

                await _issueRepository.AddAsync(issue);
                await _issueRepository.SaveChangesAsync();

                response.Data = issue;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<Issue>> GetIssueById(int id)
        {
            var response = new ServiceResponse<Issue>();
            try
            {
                var issue = await _issueRepository.GetByIdAsync(id);
                if (issue == null)
                {
                    response.Success = false;
                    response.Message = "Issue not found";
                    return response;
                }
                response.Data = issue;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ServiceResponse<Issue>> GetIssueByComicVineId(int comicVineId)
        {
            var response = new ServiceResponse<Issue>();
            try
            {
                var issue = await _issueRepository.GetByComicVineIdAsync(comicVineId);
                if (issue == null)
                {
                    response.Success = false;
                    response.Message = "Issue not found";
                    return response;
                }
                response.Data = issue;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ServiceResponse<List<Issue>>> GetAllIssues()
        {
            var response = new ServiceResponse<List<Issue>>();
            try
            {
                var issues = await _issueRepository.GetAll().ToListAsync();
                response.Data = issues;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ServiceResponse<List<Issue>>> GetIssuesByVolume(int volumeId)
        {
            var response = new ServiceResponse<List<Issue>>();
            try
            {
                var issues = await _issueRepository.GetIssuesByVolumeId(volumeId);

                if (issues == null || !issues.Any())
                {
                    response.Success = false;
                    response.Message = "No issues found for this volume";
                    return response;
                }

                response.Data = issues;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ServiceResponse<List<Issue>>> GetIssuesByReadStatus(bool readStatus)
        {
            var response = new ServiceResponse<List<Issue>>();
            try
            {
                var issues = await _issueRepository.GetReadIssues(readStatus);
                response.Data = issues;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ServiceResponse<Issue>> UpdateIssue(IssueUpdateDto issueDto)
        {
            var response = new ServiceResponse<Issue>();
            try
            {
                var issue = await _issueRepository.GetByIdAsync(issueDto.Id);
                if (issue == null)
                {
                    response.Success = false;
                    response.Message = "Issue not found";
                    return response;
                }

                // Atualiza os campos
                issue.Aliases = issueDto.Aliases;
                issue.CoverDate = issueDto.CoverDate;
                issue.Deck = issueDto.Deck;
                issue.Description = issueDto.Description;
                issue.HasStaffReview = issueDto.HasStaffReview;
                issue.ImageUrl = issueDto.ImageUrl;
                issue.IssueNumber = issueDto.IssueNumber;
                issue.Name = issueDto.Name;
                issue.SiteDetailUrl = issueDto.SiteDetailUrl;
                issue.StoreDate = issueDto.StoreDate;
                issue.VolumeDetails = issueDto.VolumeDetails;
                issue.Read = issueDto.Read;
                issue.Notes = issueDto.Notes;
                issue.Rating = issueDto.Rating;

                _issueRepository.Update(issue);
                await _issueRepository.SaveChangesAsync();

                response.Data = issue;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ServiceResponse<Issue>> MarkAsRead(int id, bool readStatus)
        {
            var response = new ServiceResponse<Issue>();
            try
            {
                var issue = await _issueRepository.GetByIdAsync(id);
                if (issue == null)
                {
                    response.Success = false;
                    response.Message = "Issue not found";
                    return response;
                }

                issue.Read = readStatus;
                _issueRepository.Update(issue);
                await _issueRepository.SaveChangesAsync();

                response.Data = issue;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteIssue(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var issue = await _issueRepository.GetByIdAsync(id);
                if (issue == null)
                {
                    response.Success = false;
                    response.Message = "Issue not found";
                    return response;
                }

                _issueRepository.Delete(issue);
                await _issueRepository.SaveChangesAsync();

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
}