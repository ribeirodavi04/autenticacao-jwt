using AutenticacaoJWT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Domain.Interfaces
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> UserExists(string email);
        public string GenerateToken(int id, string email);
        Task<User> GetUserByEmail(string email);
    }
}
