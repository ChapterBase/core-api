using ChapterBaseAPI.Data;
using ChapterBaseAPI.Models;

namespace ChapterBaseAPI.Repositories
{
    public class BookRepository(ApplicationDBContext dbContext)
    {
        internal List<Book> FindAll(int page, int size)
        {
            return dbContext.Books
                .OrderByDescending(b => b.UpdatedAt)
                .Skip(page * size)
                .Take(size)
                .ToList();
        }

        internal List<Book?> FindAllByStatus(string status)
        {
            return dbContext.Books
                .Where(b => b != null && b.Status == status)
                .ToList();
        }


        internal Book? FindById(Guid id) => dbContext.Books.FirstOrDefault(b => b != null && b.Id == id);



        internal Book? FindByIsbn(string iSbn) => dbContext.Books.FirstOrDefault(b => b != null && b.ISBN.Equals(iSbn));

        internal void Save(Book book)
        {
            dbContext.Add(book);
            dbContext.SaveChanges();
        }

        internal void Update(Book book)
        {
            dbContext.Update(book);
            dbContext.SaveChanges();
        }
    }
}
