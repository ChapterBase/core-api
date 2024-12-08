using System.IdentityModel.Tokens.Jwt;
using ChapterBaseAPI.Dto;

namespace ChapterBaseAPI.Services
{
    public class JwtUtilService
    {
        // Existing DecodeToken method
        public UserDto DecodeToken(string idToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(idToken) as JwtSecurityToken;

            if (jsonToken == null)
            {
                throw new UnauthorizedAccessException("Id Token is null. Try with token");
            }

            var UserName = jsonToken.Claims.First(claim => claim.Type == "cognito:username").Value;
            var Email = jsonToken.Claims.First(claim => claim.Type == "email").Value;

            return new UserDto
            {
                Username = UserName,
                Email = Email
            };
        }

        public UserDto DecodeToken(string idToken, bool validateIssuer)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(idToken) as JwtSecurityToken;

            if (jsonToken == null)
            {
                throw new UnauthorizedAccessException("Id Token is null. Try with token");
            }

            if (validateIssuer)
            {
                var issuer = jsonToken.Issuer;
                if (issuer != "expectedIssuer")
                {
                    throw new UnauthorizedAccessException("Invalid token issuer.");
                }
            }

            var UserName = jsonToken.Claims.First(claim => claim.Type == "cognito:username").Value;
            var Email = jsonToken.Claims.First(claim => claim.Type == "email").Value;

            return new UserDto
            {
                Username = UserName,
                Email = Email
            };
        }

        // Overloaded method to decode token and extract additional claims
        public UserDto DecodeToken(string idToken, bool validateIssuer, out string role)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(idToken) as JwtSecurityToken;

            if (jsonToken == null)
            {
                throw new UnauthorizedAccessException("Id Token is null. Try with token");
            }

            if (validateIssuer)
            {
                var issuer = jsonToken.Issuer;
                if (issuer != "expectedIssuer")
                {
                    throw new UnauthorizedAccessException("Invalid token issuer.");
                }
            }

            var UserName = jsonToken.Claims.First(claim => claim.Type == "cognito:username").Value;
            var Email = jsonToken.Claims.First(claim => claim.Type == "email").Value;
            role = jsonToken.Claims.First(claim => claim.Type == "role").Value;

            return new UserDto
            {
                Username = UserName,
                Email = Email
            };
        }
    }
}
