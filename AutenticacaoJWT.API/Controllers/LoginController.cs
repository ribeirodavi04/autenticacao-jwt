using AutenticacaoJWT.API.Models;
using AutenticacaoJWT.Application.DTO;
using AutenticacaoJWT.Application.Interfaces;
using AutenticacaoJWT.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoJWT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticate _authenticateService;
        private readonly IUserService _userService;

        public LoginController(IAuthenticate authenticateService, IUserService userService)
        {
            _authenticateService = authenticateService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> Register([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("Dados inválido.");

            var emailExists = await _authenticateService.UserExists(userDTO.Email);

            if (emailExists == true)
                return BadRequest("Email ja cadastrado.");

            var user = await _userService.AddUser(userDTO);
            if (user == null)
                return BadRequest("Erro ao cadastrar usuario.");

            var token = _authenticateService.GenerateToken(user.Id, user.Email);
            return new UserToken
            {
                Token = token,
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login(LoginModel loginModel)
        {
            var userExists = await _authenticateService.UserExists(loginModel.Email);
            if (!userExists)
                return Unauthorized("Usuario não encontrado.");

            var result = await _authenticateService.AuthenticateAsync(loginModel.Email, loginModel.Password);
            if (!result)
                return Unauthorized("Email ou senha invalido(s).");

            var user = await _authenticateService.GetUserByEmail(loginModel.Email);

            var token = _authenticateService.GenerateToken(user.Id, user.Email);

            return new UserToken { Token = token, };

        }

    }
}
