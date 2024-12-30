using ChapterBaseAPI.Dto;
using ChapterBaseAPI.Models;
using ChapterBaseAPI.Repositories;

namespace ChapterBaseAPI.Services
{
    public class CartService(
        BookRepository bookRepository,
        UserRepository userRepository,
        CartRepository cartRepository,
        CartItemRepository cartItemRepository
        )
    {

        public ResponseDto<object> Add(Guid userId, Guid bookId, int qty)
        {
            Users user = userRepository.FindById(userId);
            if (user == null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            Book book = bookRepository.FindById(bookId);
            if (book == null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Book not found"
                };
            }

            Cart cart = cartRepository.FindByUserId(userId);
            if (cart == null)
            {
                cart = new Cart();
                cart.UserId = userId;
                cart = cartRepository.Save(cart);
            }

            CartItem cartItem = cartItemRepository.FindByCartIdAndBookId(cart.Id, bookId);
            if (cartItem != null)
            {
                cartItem.Qty = qty;
                cartItemRepository.Update(cartItem);
            }
            else
            {
                cartItem = new CartItem();
                cartItem.CartId = cart.Id;
                cartItem.BookId = bookId;
                cartItem.Qty = qty;
                cartItemRepository.Save(cartItem);
            }

            return new ResponseDto<object>
            {
                Success = true,
                Message = "Cart has been successfully updated."
            };
        }


    }
}
