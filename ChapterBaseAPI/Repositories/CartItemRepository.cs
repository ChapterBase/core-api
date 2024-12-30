using ChapterBaseAPI.Data;
using ChapterBaseAPI.Models;

namespace ChapterBaseAPI.Repositories
{
    public class CartItemRepository(ApplicationDBContext dbContext)
    {

        internal CartItem FindByCartIdAndBookId(Guid cartId, Guid bookId) =>
            dbContext.CartItems
                .FirstOrDefault(ci => ci.CartId == cartId && ci.BookId == bookId);

        internal void Save(CartItem cartItem)
        {
            dbContext.Add(cartItem);
            dbContext.SaveChanges();
        }

        internal void Update(CartItem cartItem)
        {
            dbContext.Update(cartItem);
            dbContext.SaveChanges();
        }
    }
}
