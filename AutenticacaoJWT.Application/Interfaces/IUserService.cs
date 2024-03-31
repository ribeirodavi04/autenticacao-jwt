using AutenticacaoJWT.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> AddUser(UserDTO userDTO);
    }
}
