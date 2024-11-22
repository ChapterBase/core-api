using ChapterBaseAPI.Data;
using ChapterBaseAPI.Models;

namespace ChapterBaseAPI.Repositories
{
    public class BookRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public BookRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal List<Book> FindAll(int page, int size)
        {
            return _dbContext.Books.Skip(page * size).Take(size).ToList();
        }

        internal Book FindById(Guid id)
        {
            return _dbContext.Books.FirstOrDefault(b => b.Id == id);
        }

        internal Book FindByISBN(string iSBN)
        {
            return _dbContext.Books.FirstOrDefault(b => b.ISBN == iSBN);
        }

        internal void Save(Book book)
        {
            _dbContext.Add(book);
            _dbContext.SaveChanges();
        }

        internal void Update(Book book)
        {
            _dbContext.Update(book);
            _dbContext.SaveChanges();
        }
    }
}
