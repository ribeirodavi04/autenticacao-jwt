using AutenticacaoJWT.API.Models;
using AutenticacaoJWT.Application.DTO;
using AutenticacaoJWT.Application.Interfaces;
using AutenticacaoJWT.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoJWT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticate _authenticateService;

        public UserController(IUserService userService, IAuthenticate authenticate)
        {
            _userService = userService;
            _authenticateService = authenticate;
        }

        [HttpGet("{skip}/{take}")]
        public async Task<ActionResult<List<UserDTO>>> Get(int skip = 0, int take = 25)
        {
            var users = await _userService.GetAllUsers();

            var total = users.Count();

            if (users == null)
                return NotFound("Usuários não encontrados.");

            return Ok(new
            {
                total,
                data = users.Skip(skip).Take(take)
            });
        }

        [HttpGet("{idUser:int}")]
        public async Task<ActionResult<UserDTO>> Get(int idUser)
        {
            var user = await _userService.GetUserById(idUser);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("Dados inválidos");

            var userUpdated = await _userService.UpdateUser(userDTO);

            if (userUpdated == null)
                return BadRequest("Erro ao atualizar usuário.");
            
            return Ok("Usuário atualizado com sucesso.");
        }

        [HttpDelete("{idUser:int}")]
        public async Task<ActionResult> Delete(int idUser)
        {
            var user = await _userService.DeleteUser(idUser);

            if (user == null)
                return BadRequest("Erro ao excluir usuário.");

            return Ok("Usuário excluído com sucesso");
        }


        
    }
}
