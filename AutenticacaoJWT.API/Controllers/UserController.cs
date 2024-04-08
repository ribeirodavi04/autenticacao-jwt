using AutenticacaoJWT.API.Extensions;
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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticate _authenticateService;

        public UserController(IUserService userService, IAuthenticate authenticate)
        {
            _userService = userService;
            _authenticateService = authenticate;
        }

        [HttpGet("Users")]
        public async Task<ActionResult<List<UserDTO>>> Get([FromQuery] PaginationParams paginationParams)
        {
            var users = await _userService.GetAllUsers(paginationParams.PageNumber, paginationParams.PageSize);

            if (users == null)
                return NotFound("Usuários não encontrados.");

            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));
            return Ok(new
            {
                Total = users.TotalCount,
                data = users
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
            #region verifica nível de permissão do usuário logado
            var userIdClaim = int.Parse(User.FindFirst("id").Value);
            var userClaim = await _userService.GetUserById(userIdClaim);

            if (!userClaim.IsAdmin)
                return Unauthorized("Você não tem permissão para atualizar este usuário.");
            #endregion


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
            #region verifica nível de permissão do usuário logado
            var userIdClaim = int.Parse(User.FindFirst("id").Value);
            var userClaim = await _userService.GetUserById(userIdClaim);

            if(!userClaim.IsAdmin)
                return Unauthorized("Você não tem permissão para excluir este usuário.");
            #endregion


            var user = await _userService.DeleteUser(idUser);

            if (user == null)
                return BadRequest("Erro ao excluir usuário.");

            return Ok("Usuário excluído com sucesso");
        }


        
    }
}
