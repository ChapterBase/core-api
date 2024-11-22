using ChapterBaseAPI.Models;
using ChapterBaseAPI.Data;
using ChapterBaseAPI.Dtos;

namespace ChapterBaseAPI.Repositories
{
    public class UserRepository(ApplicationDBContext _dbContext)
    {
        internal void Save(Users user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }
        internal void Update(Users user)
        {
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }
        
        public Users? FindById(Guid id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        internal List<Users> FindAllByRole(string role)
        {
            return _dbContext.Users
                .OrderByDescending(u => u.UpdatedAt)
                .Where(u => u.Role == role)
                .ToList();

        }

        internal Users? FindByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

    }
}
