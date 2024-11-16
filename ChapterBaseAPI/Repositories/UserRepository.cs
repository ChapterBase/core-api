using ChapterBaseAPI.Models;
using ChapterBaseAPI.Data;
using ChapterBaseAPI.Dtos;



namespace ChapterBaseAPI.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public UserRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public Users? GetUserById(Guid id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        internal Users? FindByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        internal void Save(Users user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
