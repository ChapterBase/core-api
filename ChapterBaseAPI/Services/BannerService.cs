using ChapterBaseAPI.Dto;
using ChapterBaseAPI.Models;
using ChapterBaseAPI.Repositories;

namespace ChapterBaseAPI.Services
{
    public class BannerService(BannerRepository bannerRepository)
    {

        public ResponseDto<object> Save(BannerDto bannerDto)
        {
            var banner = new Banner
            {
                Title = bannerDto.Title,
                Description = bannerDto.Description,
                Status = bannerDto.Status,
                Image = bannerDto.Image,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            bannerRepository.Save(banner);

            return new ResponseDto<object>
            {
                Success = true,
                Message = "Banner saved successfully"
            };
        }

        public ResponseDto<object> Update(BannerDto bannerDto)
        {
            var banner = bannerRepository.FindById(bannerDto.Id);

            if (banner is null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Banner not found"
                };
            }

            banner.Title = bannerDto.Title;
            banner.Description = bannerDto.Description;
            banner.Status = bannerDto.Status;
            banner.Image = bannerDto.Image;
            banner.UpdatedAt = DateTime.Now;

            bannerRepository.Update(banner);

            return new ResponseDto<object>
            {
                Success = true,
                Message = "Banner updated successfully"
            };
        }

        public List<BannerDto> FindAll(string status)
        {
            return bannerRepository
                .FindAll(status)
                .Select(MapToDto)
                .ToList();
        }

        private static BannerDto MapToDto(Banner banner)
        {
            return new BannerDto
            {
                Id = banner.Id,
                Title = banner.Title,
                Description = banner.Description,
                Status = banner.Status,
                Image = banner.Image,
                CreatedAt = banner.CreatedAt,
                UpdatedAt = banner.UpdatedAt
            };
        }


    }
}
