using ChapterBaseAPI.Dtos;
using ChapterBaseAPI.Models;
using ChapterBaseAPI.Repositories;

namespace ChapterBaseAPI.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtUtilService _jwtUtilService;


        public UserService(UserRepository userRepository, JwtUtilService jwtUtilService)
        {
            this._userRepository = userRepository;
            this._jwtUtilService = jwtUtilService;
        }

        public void save(String idToken)
        {
            UserDto UserDto = _jwtUtilService.DecodeToken(idToken);
            Users User = _userRepository.FindByEmail(UserDto.Email);

            if (User != null) return;

            _userRepository.Save(
                new Users
                {
                    Username = UserDto.Username,
                    Email = UserDto.Email,
                    Role = "USER",
                    Status = "ACTIVE",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });


        }

        public List<UserDto> GetAllUsers()
        {

            IEnumerable<Users> enumerable = _userRepository.GetAllUsers();

            List<UserDto> userDtos = _userRepository.GetAllUsers()
                 .Select(user => new UserDto
                 {
                     Id = user.Id,
                     Username = user.Username,
                     Email = user.Email,
                 }
                 ).ToList();
            System.Console.WriteLine("Database returned user list");
            System.Console.WriteLine(userDtos);

            return userDtos;
        }

    }
}
