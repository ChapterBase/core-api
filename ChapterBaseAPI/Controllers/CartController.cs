using ChapterBaseAPI.Dto;
using ChapterBaseAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChapterBaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController(CartService cartService): Controller
    {
        [HttpPost]
        public IActionResult Add([FromQuery] Guid userId, [FromQuery] Guid bookId, [FromQuery] int qty)
        {
            return Ok(cartService.Add(userId, bookId, qty));
        }

    }
}
