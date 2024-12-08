using admin_bff.Dto;
using ChapterBaseAPI.Dto;
using ChapterBaseAPI.Migrations;
using ChapterBaseAPI.Models;
using ChapterBaseAPI.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ChapterBaseAPI.Services;

public class BookService(BookRepository bookRepository)
{
    public ResponseDto<object> Save(BookDto bookDto)
    {
        var book = bookRepository.FindByIsbn(bookDto.ISBN);

        if (book != null)
            return new ResponseDto<object>
            {
                Success = false,
                Message = "Book already exists for ISBN " + bookDto.ISBN
            };

        bookRepository.Save(
            new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                ISBN = bookDto.ISBN,
                Publisher = bookDto.Publisher,
                Quantity = bookDto.Quantity,
                Price = bookDto.Price,
                Image = bookDto.Image,
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
        ValidateRequest(bookDto);

        var book = bookRepository.FindById(bookDto.Id);

        if (book == null)
            return new ResponseDto<object>
            {
                Success = false,
                Message = "Book not found"
            };

        book.Title = bookDto.Title;
        book.Author = bookDto.Author;
        book.Publisher = bookDto.Publisher;
        book.Quantity = bookDto.Quantity;
        book.Price = bookDto.Price;
        book.Status = bookDto.Status;
        book.Image = bookDto.Image;
        book.PublishedDate = bookDto.PublishedDate;
        book.UpdatedAt = DateTime.Now;

        bookRepository.Update(book);

        return new ResponseDto<object>
        {
            Success = true,
            Message = "Book updated successfully"
        };
    }

    private static void ValidateRequest(BookDto bookDto)
    {
        if (bookDto.Id == Guid.Empty)
            throw new Exception("Book ID is required");

        if (string.IsNullOrEmpty(bookDto.Title))
            throw new Exception("Book title is required");

        if (string.IsNullOrEmpty(bookDto.Author))
            throw new Exception("Book author is required");

        
    }

    public ResponseDto<object> FindAll(RequestDto request)
    {
        List<BookDto> books;
        if ("ALL".Equals(request.Status))
            books = bookRepository
                .FindAll(request.Page, request.Size)
                .Select(ConvertBook)
                .ToList();
        else
            books = bookRepository
                .FindAllByStatus(request.Status)
                .Select(ConvertBook)
                .ToList();
        return new ResponseDto<object>
        {
            Success = true,
            Data = books
        };
    }

    public ResponseDto<object> FindById(Guid id)
    {
        var book = bookRepository.FindById(id);

        if (book == null)
            return new ResponseDto<object>
            {
                Success = false,
                Message = "Book not found"
            };

        return new ResponseDto<object>
        {
            Success = true,
            Data = ConvertBook(book)
        };
    }

    public ResponseDto<object> Delete(Guid id)
    {
        var book = bookRepository.FindById(id);

        if (book == null)
            return new ResponseDto<object>
            {
                Success = false,
                Message = "Book not found"
            };

        book.Status = "DELETED";
        book.UpdatedAt = DateTime.Now;

        bookRepository.Update(book);

        return new ResponseDto<object>
        {
            Success = true,
            Message = "Book deleted successfully"
        };
    }


    private static BookDto ConvertBook(Book? book)
    {
        if (book == null)
            return new BookDto();
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
            Image = book.Image,
            PublishedDate = book.PublishedDate,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }
}