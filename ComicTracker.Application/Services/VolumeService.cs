using ComicTracker.Application.DTOs;
using ComicTracker.Application.DTOs.ComicVine;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Application.Services;

public class VolumeService : IVolumeService
{
    private readonly IVolumeRepository _volumeRepository;
    private readonly IComicVineService _comicVineService;
    private readonly IPublisherRepository _publisherRepository;

    public VolumeService(
        IVolumeRepository volumeRepository,
        IComicVineService comicVineService,
        IPublisherRepository publisherRepository)
    {
        _volumeRepository = volumeRepository;
        _comicVineService = comicVineService;
        _publisherRepository = publisherRepository;
    }

    public async Task<ServiceResponse<List<ComicVineVolume>>> SearchVolumes(string name)
    {
        var response = new ServiceResponse<List<ComicVineVolume>>();

        try
        {
            var comicVineResponse = await _comicVineService.GetVolumes($"name:{name}");

            if (comicVineResponse.Error != "OK")
            {
                response.Success = false;
                response.Message = "Error from Comic Vine API";
                return response;
            }

            var existingIds = await _volumeRepository.GetAll()
                .Where(v => comicVineResponse.Results.Select(cv => cv.Id).Contains(v.ComicVineId))
                .Select(v => v.ComicVineId)
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

    public async Task<ServiceResponse<Volume>> CreateVolume(VolumeCreateDto volumeDto)
    {
        var response = new ServiceResponse<Volume>();

        try
        {
            if (await _volumeRepository.ExistsByComicVineId(volumeDto.ComicVineId))
            {
                response.Success = false;
                response.Message = "Volume already exists";
                return response;
            }

            var publisher = await _publisherRepository.GetByIdAsync(volumeDto.PublisherId);
            if (publisher == null)
            {
                response.Success = false;
                response.Message = "Publisher not found";
                return response;
            }

            var volume = new Volume
            {
                ComicVineId = volumeDto.ComicVineId,
                Name = volumeDto.Name,
                Aliases = volumeDto.Aliases,
                CountOfIssues = volumeDto.CountOfIssues,
                Deck = volumeDto.Deck,
                Description = volumeDto.Description,
                FirstIssue = volumeDto.FirstIssue,
                ImageUrl = volumeDto.ImageUrl,
                LastIssue = volumeDto.LastIssue,
                PublisherId = volumeDto.PublisherId,
                SiteDetailUrl = volumeDto.SiteDetailUrl,
                StartYear = volumeDto.StartYear
            };

            await _volumeRepository.AddAsync(volume);
            await _volumeRepository.SaveChangesAsync();

            response.Data = volume;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    public async Task<ServiceResponse<Volume>> GetVolumeById(int id)
    {
        var response = new ServiceResponse<Volume>();
        try
        {
            var volume = await _volumeRepository.GetByIdAsync(id);
            if (volume == null)
            {
                response.Success = false;
                response.Message = "Volume not found";
                return response;
            }
            response.Data = volume;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<Volume>> GetVolumeByComicVineId(int comicVineId)
    {
        var response = new ServiceResponse<Volume>();
        try
        {
            var volume = await _volumeRepository.GetByComicVineIdAsync(comicVineId);
            if (volume == null)
            {
                response.Success = false;
                response.Message = "Volume not found";
                return response;
            }
            response.Data = volume;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<List<Volume>>> GetAllVolumes()
    {
        var response = new ServiceResponse<List<Volume>>();
        try
        {
            var volumes = await _volumeRepository.GetAll().ToListAsync();
            response.Data = volumes;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<List<Volume>>> GetVolumesByPublisher(int publisherId)
    {
        var response = new ServiceResponse<List<Volume>>();
        try
        {
            var volumes = await _volumeRepository.GetVolumesByPublisherId(publisherId);
            response.Data = volumes;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<Volume>> UpdateVolume(VolumeUpdateDto volumeDto)
    {
        var response = new ServiceResponse<Volume>();
        try
        {
            var volume = await _volumeRepository.GetByIdAsync(volumeDto.Id);
            if (volume == null)
            {
                response.Success = false;
                response.Message = "Volume not found";
                return response;
            }

            // Atualiza os campos
            volume.Name = volumeDto.Name;
            volume.Aliases = volumeDto.Aliases;
            volume.CountOfIssues = volumeDto.CountOfIssues;
            volume.Deck = volumeDto.Deck;
            volume.Description = volumeDto.Description;
            volume.FirstIssue = volumeDto.FirstIssue;
            volume.ImageUrl = volumeDto.ImageUrl;
            volume.LastIssue = volumeDto.LastIssue;
            volume.SiteDetailUrl = volumeDto.SiteDetailUrl;
            volume.StartYear = volumeDto.StartYear;

            _volumeRepository.Update(volume);
            await _volumeRepository.SaveChangesAsync();

            response.Data = volume;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }
        return response;
    }

    public async Task<ServiceResponse<bool>> DeleteVolume(int id)
    {
        var response = new ServiceResponse<bool>();
        try
        {
            var volume = await _volumeRepository.GetByIdAsync(id);
            if (volume == null)
            {
                response.Success = false;
                response.Message = "Volume not found";
                return response;
            }

            _volumeRepository.Delete(volume);
            await _volumeRepository.SaveChangesAsync();

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