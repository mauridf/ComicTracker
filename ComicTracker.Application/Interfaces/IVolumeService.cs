using ComicTracker.Application.DTOs;
using ComicTracker.Domain.Entities;

namespace ComicTracker.Application.Interfaces;

public interface IVolumeService
{
    Task<ServiceResponse<List<ComicVineVolume>>> SearchVolumes(string name);
    Task<ServiceResponse<Volume>> CreateVolume(VolumeCreateDto volumeDto);
    Task<ServiceResponse<Volume>> GetVolumeById(int id);
    Task<ServiceResponse<Volume>> GetVolumeByComicVineId(int comicVineId);
    Task<ServiceResponse<List<Volume>>> GetAllVolumes();
    Task<ServiceResponse<List<Volume>>> GetVolumesByPublisher(int publisherId);
    Task<ServiceResponse<Volume>> UpdateVolume(VolumeUpdateDto volumeDto);
    Task<ServiceResponse<bool>> DeleteVolume(int id);
}
