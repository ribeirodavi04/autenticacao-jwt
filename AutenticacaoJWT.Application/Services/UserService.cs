using AutenticacaoJWT.Application.DTO;
using AutenticacaoJWT.Application.Interfaces;
using AutenticacaoJWT.Domain.Entities;
using AutenticacaoJWT.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<UserDTO>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> AddUser(UserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            var newUser = await _userRepository.Create(user);
            return _mapper.Map<UserDTO>(newUser);
        }
    }
}
