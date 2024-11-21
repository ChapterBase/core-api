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

        internal Book FindByISBN(string iSBN)
        {
            return _dbContext.Books.FirstOrDefault(b => b.ISBN == iSBN);
        }

        internal void Save(Book book)
        {
            _dbContext.Add(book);
            _dbContext.SaveChanges();
        }
    }
}
