using AutenticacaoJWT.Application.DTO;
using AutenticacaoJWT.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoJWT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("Invalid Data");

            await _userService.Add(userDTO);
            return Ok();
        }
    }
}
