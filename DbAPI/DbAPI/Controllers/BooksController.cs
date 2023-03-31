using DbAPI.Models;
using DbAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DbAPI.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookDbService _bookDbService;

    public BooksController(IBookDbService bookDbService)
    {
        _bookDbService = bookDbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string title)
    {
        IList<Book> books = await _bookDbService.GetBooksListAsync(title);
        return Ok(books);
    }
}