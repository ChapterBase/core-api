using ChapterBaseAPI.Dtos;
using ChapterBaseAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChapterBaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(UserService userService) : Controller
    {
        [HttpPost]
        public IActionResult Save([FromBody] UserDto userDto)
        {
            return Ok(userService.Save(userDto));
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserDto userDto)
        {
            return Ok(userService.Update(userDto));
        }

        [HttpGet("By/Role")]
        public IActionResult FindAllByRole([FromQuery] string role)
        {
            return Ok(userService.FindAllByRole(role));
        }

        [HttpGet("{id}")]
        public IActionResult FindById([FromRoute] Guid id)
        {
            return Ok(userService.FindById(id));
        }

    }
}
