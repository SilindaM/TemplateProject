namespace API.Controllers.Auth
{
  using Application.Interfaces.Auth;
  using Domain.Constants;
  using Domain.Dtos.Account;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;

  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
      this.authService = authService;
    }

    [HttpPost]
    [Route("seed-roles")]
    public async Task<IActionResult> SeedRoles()
    {
      var seedResults = await authService.SeedRolesAsync();
      return StatusCode(seedResults.StatusCode, seedResults.Message);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
      var registerResult = await authService.RegisterAsync(registerDto);
      return StatusCode(registerResult.StatusCode, registerResult.Message);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
      var loginResult = await authService.LoginAsync(loginDto);
      if(loginResult is null)
      {
        return Unauthorized("Invalid Credentials");
      }
      return Ok(loginResult);
    }

    [HttpPost]
    [Route("UpdateRoles")]
    [Authorize(Roles =StaticUserRoles.OwnerAdmin)]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDto updateRoleDto)
    {
      var updateRoleResult = await authService.UpdateRoleAsync(User, updateRoleDto);
      if (updateRoleResult.IsSucceed)
      {
        return Ok(updateRoleResult.Message);
      }
      else
      {
        return StatusCode(updateRoleResult.StatusCode, updateRoleResult.Message);
      }
    }
    [HttpPost]
    [Route("me")]
    public async Task<ActionResult<LoginServiceResponseDto>> Me([FromBody] MeDto token)
    {
      try
      {
        var me = await authService.MeAsync(token);
        if (me is null)
        {
          return Ok(me);
        }
        else
        {
          return Unauthorized("Invalid Token");
        }
      }
      catch (Exception ex)
      {
        return Unauthorized("Invalid Token");
      }
      }
    [HttpGet]
    [Route("users")]
    public async Task<ActionResult<IEnumerable<UserInfoResult>>> GetUsersList()
    {
      var userList = await authService.GetUserListAsync();
      return Ok(userList);
    }

    [HttpGet]
    [Route("users/{username}")]
    public async Task<ActionResult<UserInfoResult>> GetUserDetailsByUsername([FromRoute] string username)
    {
      var user = await authService.GetUserDetailsByUserNamesync(username);
      if(user is null)
      {
        return Ok(user);
      }
      else
      {
        return NotFound("Username not found");
      }
    }
    [HttpGet]
    [Route("usernames")]
    public async Task<ActionResult<IEnumerable<string>>> GetUsernameList()
    {
      var usernames = await authService.GetUsernamesListAsync();
      return Ok(usernames);
    }
  }
}