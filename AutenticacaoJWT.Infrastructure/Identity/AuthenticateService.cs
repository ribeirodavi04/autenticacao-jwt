using AutenticacaoJWT.Domain.Entities;
using AutenticacaoJWT.Domain.Interfaces;
using AutenticacaoJWT.Infra.Data.Context;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly ContextApp _context;
        private readonly IConfiguration _configuration;

        public AuthenticateService(ContextApp context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateAsync(string email, string passwordInput)
        {
            var user =  _context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();
            
            if (user == null) 
                return false;
        
            string salt = Convert.ToBase64String(user.Salt);
            string password = EncryptPassword(passwordInput, user.Salt);

            if (user.Password != password)
                return false;

            return true;
        }

        public string GenerateToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExists(string email)
        {
            var user = _context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();

            if (user == null)
                return false;

            return true;
        }

        private string EncryptPassword(string password, byte[] salt)
        {
            string cryptoPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return cryptoPassword;
        }
    }
}
