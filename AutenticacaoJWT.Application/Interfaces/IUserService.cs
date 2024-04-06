using AutenticacaoJWT.Application.DTO;
using AutenticacaoJWT.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Application.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserDTO>> GetAllUsers(int pageNumber, int pageSize);
        Task<UserDTO> GetUserById(int idUser);
        Task<UserDTO> AddUser(UserDTO userDTO);
        Task<UserDTO> UpdateUser(UserDTO userDTO);
        Task<UserDTO> DeleteUser(int idUser);
    }
}
