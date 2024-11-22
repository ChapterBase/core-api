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

        [HttpPut]
        public IActionResult Update([FromBody] BookDto bookDto)
        {
            return Ok(_bookService.Update(bookDto));
        }

        [HttpGet]
        public IActionResult FindAll([FromQuery] int page, [FromQuery] int size)
        {
            return Ok(_bookService.FindAll(page, size));
        }

        [HttpGet("{id}")]
        public IActionResult FindById([FromRoute] Guid id)
        {
            return Ok(_bookService.FindById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            return Ok(_bookService.Delete(id));
        }
    }
}
