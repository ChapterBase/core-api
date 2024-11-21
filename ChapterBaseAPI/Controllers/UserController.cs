using ChapterBaseAPI.Dtos;
using ChapterBaseAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChapterBaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
    
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAllUsers());
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] UserDto userDto)
        {
            _userService.Save(userDto);
            return Ok();
        }


    }
}
