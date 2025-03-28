using ComicTracker.Application.DTOs.ComicVine;
using ComicTracker.Application.DTOs;
using ComicTracker.Application.Interfaces;
using ComicTracker.Domain.Entities;
using ComicTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComicTracker.Application.Services;

public class CharacterService : ICharacterService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IComicVineService _comicVineService;

    public CharacterService(
        ICharacterRepository characterRepository,
        IComicVineService comicVineService)
    {
        _characterRepository = characterRepository;
        _comicVineService = comicVineService;
    }

    public async Task<ServiceResponse<List<ComicVineCharacter>>> SearchCharacters(string name)
    {
        var response = new ServiceResponse<List<ComicVineCharacter>>();

        try
        {
            var comicVineResponse = await _comicVineService.GetCharacters($"name:{name}");

            if (comicVineResponse.Error != "OK")
            {
                response.Success = false;
                response.Message = "Error from Comic Vine API";
                return response;
            }

            var existingIds = await _characterRepository.GetAll()
                .Where(c => comicVineResponse.Results.Select(cv => cv.Id).Contains(c.ComicVineId))
                .Select(c => c.ComicVineId)
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

    public async Task<ServiceResponse<Character>> CreateCharacter(CharacterCreateDto characterDto)
    {
        var response = new ServiceResponse<Character>();

        try
        {
            if (await _characterRepository.ExistsByComicVineId(characterDto.ComicVineId))
            {
                response.Success = false;
                response.Message = "Character already exists";
                return response;
            }

            var character = new Character
            {
                ComicVineId = characterDto.ComicVineId,
                Name = characterDto.Name,
                Aliases = characterDto.Aliases,
                Birth = characterDto.Birth,
                CountOfIssueAppearances = characterDto.CountOfIssueAppearances,
                Deck = characterDto.Deck,
                Description = characterDto.Description,
                FirstAppearedInIssue = characterDto.FirstAppearedInIssue,
                Gender = characterDto.Gender,
                ImageUrl = characterDto.ImageUrl,
                Origin = characterDto.Origin,
                PublisherName = characterDto.PublisherName,
                RealName = characterDto.RealName,
                SiteDetailUrl = characterDto.SiteDetailUrl
            };

            await _characterRepository.AddAsync(character);
            await _characterRepository.SaveChangesAsync();

            response.Data = character;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    public async Task<ServiceResponse<Character>> GetCharacterById(int id)
    {
        var response = new ServiceResponse<Character>();

        try
        {
            var character = await _characterRepository.GetByIdAsync(id);

            if (character == null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            response.Data = character;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
    {
        var response = new ServiceResponse<List<Character>>();

        try
        {
            var characters = await _characterRepository.GetAll().ToListAsync();
            response.Data = characters;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    public async Task<ServiceResponse<Character>> UpdateCharacter(CharacterUpdateDto characterDto)
    {
        var response = new ServiceResponse<Character>();

        try
        {
            var character = await _characterRepository.GetByIdAsync(characterDto.Id);

            if (character == null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            // Atualiza apenas os campos que podem ser modificados
            character.Name = characterDto.Name;
            character.Aliases = characterDto.Aliases;
            character.Birth = characterDto.Birth;
            character.CountOfIssueAppearances = characterDto.CountOfIssueAppearances;
            character.Deck = characterDto.Deck;
            character.Description = characterDto.Description;
            character.FirstAppearedInIssue = characterDto.FirstAppearedInIssue;
            character.Gender = characterDto.Gender;
            character.ImageUrl = characterDto.ImageUrl;
            character.Origin = characterDto.Origin;
            character.PublisherName = characterDto.PublisherName;
            character.RealName = characterDto.RealName;
            character.SiteDetailUrl = characterDto.SiteDetailUrl;

            _characterRepository.Update(character);
            await _characterRepository.SaveChangesAsync();

            response.Data = character;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> DeleteCharacter(int id)
    {
        var response = new ServiceResponse<bool>();

        try
        {
            var character = await _characterRepository.GetByIdAsync(id);

            if (character == null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            _characterRepository.Delete(character);
            await _characterRepository.SaveChangesAsync();

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

    public async Task<ServiceResponse<Character>> GetCharacterByComicVineId(int comicVineId)
    {
        var response = new ServiceResponse<Character>();

        try
        {
            var character = await _characterRepository.GetAll()
                .FirstOrDefaultAsync(c => c.ComicVineId == comicVineId);

            if (character == null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            response.Data = character;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.Errors.Add(ex.Message);
        }

        return response;
    }
}