using API.Extensions;
using Application.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
        private readonly IAuthenticationService authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

     [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
      var result = await authenticationService.Login(request);

      var resultDto = result.ToResultDto();

      if (!resultDto.IsSuccess)
      {
        return BadRequest(resultDto);
      }
      return Ok(resultDto);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
      var result = await authenticationService.Register(request);

      var resultDto = result.ToResultDto();

      if (!resultDto.IsSuccess)
      {
        return BadRequest(resultDto);
      }
      return Ok(resultDto);
    }
  }
}
