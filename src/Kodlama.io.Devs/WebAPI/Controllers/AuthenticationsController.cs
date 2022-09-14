using Application.Features.Authentications.Commands.Login;
using Application.Features.Authentications.Commands.Register;
using Application.Features.Authentications.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            CreatedUserDto result = await Mediator.Send(registerCommand);
            return Created("", result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            AccessTokenDto result = await Mediator.Send(loginCommand);
            return Created("", result);
        }
    }
}
