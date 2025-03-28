using ComicTracker.Application.DTOs.ComicVine;
using ComicTracker.Application.DTOs;
using ComicTracker.Domain.Entities;

namespace ComicTracker.Application.Interfaces;

public interface ICharacterService
{
    Task<ServiceResponse<List<ComicVineCharacter>>> SearchCharacters(string name);
    Task<ServiceResponse<Character>> CreateCharacter(CharacterCreateDto characterDto);
    Task<ServiceResponse<Character>> GetCharacterById(int id);
    Task<ServiceResponse<List<Character>>> GetAllCharacters();
    Task<ServiceResponse<Character>> UpdateCharacter(CharacterUpdateDto characterDto);
    Task<ServiceResponse<bool>> DeleteCharacter(int id);
    Task<ServiceResponse<Character>> GetCharacterByComicVineId(int comicVineId);
}