using admin_bff.Exceptions;
using ChapterBaseAPI.Dtos;
using ChapterBaseAPI.Models;
using ChapterBaseAPI.Repositories;

namespace ChapterBaseAPI.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;

        public BookService(BookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        public ResponseDto<object> Save(BookDto bookDto)
        {
            Book book = _bookRepository.FindByISBN(bookDto.ISBN);

            if (book != null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Book already exists for ISBN " + bookDto.ISBN
                };
            }
            _bookRepository.Save(
                new Book
                {
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    ISBN = bookDto.ISBN,
                    Publisher = bookDto.Publisher,
                    Quantity = bookDto.Quantity,
                    Price = bookDto.Price,
                    Status = "PREVIEW",
                    PublishedDate = bookDto.PublishedDate,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                });
            return new ResponseDto<object>
            {
                Success = true,
                Message = "Book created successfully"
            };
        }

        public ResponseDto<object> Update(BookDto bookDto)
        {
            Book book = _bookRepository.FindById(bookDto.Id);

            if (book == null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Book not found"
                };
            }

            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.Publisher = bookDto.Publisher;
            book.Quantity = bookDto.Quantity;
            book.Price = bookDto.Price;
            book.Status = bookDto.Status;
            book.PublishedDate = bookDto.PublishedDate;
            book.UpdatedAt = DateTime.Now;

            _bookRepository.Update(book);

            return new ResponseDto<object>
            {
                Success = true,
                Message = "Book updated successfully"
            };
        }

        public ResponseDto<object> FindAll(int page, int size)
        {
            return new ResponseDto<object>
            {
                Success = true,
                Data = _bookRepository
                .FindAll(page, size)
                .Select(book => convertBook(book))
                .ToList()

            };
        }

        public ResponseDto<object> FindById(Guid id)
        {
            Book book = _bookRepository.FindById(id);

            if (book == null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Book not found"
                };
            }

            return new ResponseDto<object>
            {
                Success = true,
                Data = convertBook(book)
            };
        }

        public ResponseDto<object> Delete(Guid id)
        {
            Book book = _bookRepository.FindById(id);

            if (book == null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Book not found"
                };
            }

            book.Status = "DELETED";
            book.UpdatedAt = DateTime.Now;

            _bookRepository.Update(book);

            return new ResponseDto<object>
            {
                Success = true,
                Message = "Book deleted successfully"
            };
        }
        

        private BookDto convertBook(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Publisher = book.Publisher,
                Quantity = book.Quantity,
                Price = book.Price,
                Status = book.Status,
                PublishedDate = book.PublishedDate,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt
            };
        }



    }
}
