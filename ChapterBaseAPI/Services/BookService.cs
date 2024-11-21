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

        public void Save(BookDto bookDto)
        {
            Book book = _bookRepository.FindByISBN(bookDto.ISBN);

            if (book != null) return;
            
            _bookRepository.Save(
                new Book
                {
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    ISBN = bookDto.ISBN,
                    Publisher = bookDto.Publisher,
                    Quantity = bookDto.Quantity,
                    Price = bookDto.Price,
                    PublishedDate = bookDto.PublishedDate,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                    
                });
        }

    }
}
