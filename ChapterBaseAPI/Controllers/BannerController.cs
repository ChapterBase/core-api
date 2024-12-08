using ChapterBaseAPI.Dto;
using ChapterBaseAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChapterBaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BannerController(BannerService bannerService) : Controller
    {

        [HttpPost]
        public IActionResult Save([FromBody] BannerDto bannerDto)
        {
            return Ok(bannerService.Save(bannerDto));
        }

        [HttpPut]
        public IActionResult Update([FromBody] BannerDto bannerDto)
        {
            return Ok(bannerService.Update(bannerDto));
        }


        [HttpGet("By/Status")]
        public IActionResult FindAll([FromQuery] string status)
        {
            return Ok(bannerService.FindAll(status));
            
        }


    }
}
