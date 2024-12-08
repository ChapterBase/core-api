
using admin_bff.Dto;
using ChapterBaseAPI.Dto;
using ChapterBaseAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChapterBaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController(BookService bookService) : Controller
    {
        [HttpPost]
        public IActionResult Save([FromBody] BookDto bookDto)
        {
            return Ok(bookService.Save(bookDto));
        }

        [HttpPut]
        public IActionResult Update([FromBody] BookDto bookDto)
        {
            return Ok(bookService.Update(bookDto));
        }

        [HttpPost("All")]
        public IActionResult FindAll([FromBody] RequestDto request)
        {
            return Ok(bookService.FindAll(request));
        }

        [HttpGet("{id}")]
        public IActionResult FindById([FromRoute] Guid id)
        {
            return Ok(bookService.FindById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            return Ok(bookService.Delete(id));
        }
    }
}
