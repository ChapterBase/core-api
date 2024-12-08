using ChapterBaseAPI.Data;
using ChapterBaseAPI.Models;

namespace ChapterBaseAPI.Repositories
{
    public class BannerRepository(ApplicationDBContext dbContext)
    {

        public List<Banner?> FindAll(string status)
        {
            return dbContext.Banners
                .Where(b => b.Status == status)
                .ToList();
        }

        public void Save(Banner banner)
        {
            dbContext.Add(banner);
            dbContext.SaveChanges();
        }

        public Banner? FindById(Guid id) => dbContext.Banners.FirstOrDefault(b => b.Id == id);

        public void Update(Banner banner)
        {
            dbContext.Update(banner);
            dbContext.SaveChanges();
        }
    }
}
