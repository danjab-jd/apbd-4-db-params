using DbAPI.Models;

namespace DbAPI.Services;

public interface IBookDbService
{
    Task<IList<Book>> GetBooksListAsync(string title);
}