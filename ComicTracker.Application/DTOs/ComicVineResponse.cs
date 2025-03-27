namespace ComicTracker.Application.DTOs;

public class ComicVineResponse<T>
{
    public string Error { get; set; }
    public int NumberOfPageResults { get; set; }
    public int NumberOfTotalResults { get; set; }
    public List<T> Results { get; set; }
}