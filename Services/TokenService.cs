using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Pilllar.Admin.Models;
using Microsoft.IdentityModel.Tokens;

namespace Pilllar.Admin.Services
{
    public static class TokenService
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public static bool ValidatePassword(string textPassword, string cryptPassword)
        {
            return BCrypt.BCryptHelper.CheckPassword(textPassword, cryptPassword);
        }

        public static string EncryptPassword(string password)
        {
            string salt = BCrypt.BCryptHelper.GenerateSalt(BCrypt.SaltRevision.Revision2);

            return BCrypt.BCryptHelper.HashPassword(password, salt);
        }

    }
}