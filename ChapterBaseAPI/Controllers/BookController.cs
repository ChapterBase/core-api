using ChapterBaseAPI.Dtos;
using ChapterBaseAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChapterBaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult Save([FromBody] BookDto bookDto)
        {
            return Ok(_bookService.Save(bookDto));
        }
    }
}
