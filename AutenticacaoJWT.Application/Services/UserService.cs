using AutenticacaoJWT.Application.DTO;
using AutenticacaoJWT.Application.Interfaces;
using AutenticacaoJWT.Domain.Entities;
using AutenticacaoJWT.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserById(int idUser)
        {
            var user = await _userRepository.GetById(idUser);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> AddUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            byte[] salt = new byte[128 / 8];
            user.Salt = GenerateSalt(salt);
            
            string cryptoPassword = EncryptPassword(userDTO.Password, user.Salt);
            user.Password = cryptoPassword;

            var newUser = await _userRepository.Create(user);
            return _mapper.Map<UserDTO>(newUser);
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDTO)
        {
            var user = await _userRepository.GetById(userDTO.Id);

            if (user == null)
                return null;

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.IsAdmin = userDTO.IsAdmin;

            byte[] salt = new byte[128 / 8];
            user.Salt = GenerateSalt(salt);

            string cryptoPassword = EncryptPassword(userDTO.Password, user.Salt);
            user.Password = cryptoPassword;

            var userUpdate = await _userRepository.Update(user);
            return _mapper.Map<UserDTO>(userUpdate);
        }

        public async Task<UserDTO> DeleteUser(int idUser)
        {
            var user = await _userRepository.GetById(idUser);

            if (user == null) 
                return null;
                        
            var userDeleted = await _userRepository.Remove(user);
            return _mapper.Map<UserDTO>(userDeleted);
        }

        private Byte[] GenerateSalt(byte[] salt)
        {
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
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
