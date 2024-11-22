using ChapterBaseAPI.Dtos;
using ChapterBaseAPI.Models;
using ChapterBaseAPI.Repositories;

namespace ChapterBaseAPI.Services;

public class UserService(UserRepository userRepository, JwtUtilService jwtUtilService)
{
    public ResponseDto<object> Save(UserDto userDto)
    {
        var user = userRepository.FindByEmail(userDto.Email);

        if (user != null)
            return new ResponseDto<object>
            {
                Success = false,
                Message = "User already exists for email " + userDto.Email
            };

        userRepository.Save(
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
            Message = "User created successfully"
        };
    }

    // update user
    public ResponseDto<object> Update(UserDto userDto)
    {
        var user = userRepository.FindById(userDto.Id);

        if (user == null)
            return new ResponseDto<object>
            {
                Success = false,
                Message = "User not found"
            };

        user.Username = userDto.Username;
        user.Email = userDto.Email;
        user.Role = userDto.Role;
        user.Status = userDto.Status;
        user.UpdatedAt = DateTime.Now;

        userRepository.Update(user);

        return new ResponseDto<object>
        {
            Success = true,
            Message = "User updated successfully"
        };
    }

    internal ResponseDto<object> FindAllByRole(string role)
    {
        return new ResponseDto<object>
        {
            Success = true,
            Data = userRepository
                .FindAllByRole(role)
                .Select(ConvertUser)
                .ToList()
        };
    }

    internal ResponseDto<object> FindById(Guid id)
    {
        var user = userRepository.FindById(id);

        if (user == null)
            return new ResponseDto<object>
            {
                Success = false,
                Message = "User not found"
            };

        return new ResponseDto<object>
        {
            Success = true,
            Data = ConvertUser(user)
        };
    }

    private UserDto ConvertUser(Users user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            Status = user.Status,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}