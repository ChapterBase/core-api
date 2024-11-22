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

        public ResponseDto<object> Save(UserDto userDto)
        {
            Users User = _userRepository.FindByEmail(userDto.Email);

            if (User != null)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "User already exists for email " + userDto.Email,
                    Data = null
                };
            }

            _userRepository.Save(
                new Users
                {
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Role = userDto.Role,
                    Status = "ACTIVE",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

            return new ResponseDto<object>
            {
                Success = true,
                Message = "User created successfully",
                Data = null
            };
        }

    }
}
