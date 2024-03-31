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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticate _authenticateService;

        public UserController(IUserService userService, IAuthenticate authenticate)
        {
            _userService = userService;
            _authenticateService = authenticate;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var users = await _userService.GetAll();
            var total = users.Count();

            if (users == null)
                return NotFound("Users not Found");

            return Ok(new
            {
                total,
                data = users
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> RegisterUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("Invalid Data");

            var emailExists = await _authenticateService.UserExists(userDTO.Email);

            if (emailExists == true)
                return BadRequest("Email already exists");

            var user = await _userService.AddUser(userDTO);
            if (user == null)
                return BadRequest("Error while registering");

            var token = _authenticateService.GenerateToken(user.Id, user.Email);
            return new UserToken
            {
                Token = token,
            };
        }
    }
}
