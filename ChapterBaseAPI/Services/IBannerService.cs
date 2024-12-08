using ChapterBaseAPI.Dto;

namespace ChapterBaseAPI.Services
{
    public interface IBannerService
    {
        void DisplayBanner();
        BannerDto GetBannerById(Guid id);
        void CreateBanner(BannerDto banner);
        void UpdateBanner(BannerDto banner);
        void DeleteBanner(Guid id);
    }
}
