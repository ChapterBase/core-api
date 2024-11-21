using ChapterBaseAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        // create a post request to save user, get idToken from request params
        [HttpPost]
        public IActionResult Post([FromQuery] string idToken)
        {
            _userService.save(idToken);
            return Ok();
        }
       
    }
}
