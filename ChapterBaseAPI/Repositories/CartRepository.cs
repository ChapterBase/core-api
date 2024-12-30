using ChapterBaseAPI.Data;
using ChapterBaseAPI.Models;


namespace ChapterBaseAPI.Repositories
{
    public class CartRepository(ApplicationDBContext dbContext)
    {
        public Cart? FindByUserId(Guid userId) => dbContext.Carts.FirstOrDefault(b => b.UserId == userId);

        internal Cart Save(Cart cart)
        {
            dbContext.Add(cart);
            dbContext.SaveChanges();
            return cart;
        }
    }
}
