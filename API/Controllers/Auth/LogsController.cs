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
  public class LogsController : ControllerBase
  {
    private readonly ILogService logService;

    public LogsController(ILogService logService)
    {
      this.logService = logService;
    }

    [HttpGet]
    [Authorize(Roles = StaticUserRoles.OwnerAdmin)]
    public async Task<ActionResult<IEnumerable<GetLogDto>>> GetLogs()
    {
      var logs = await logService.getLogsAsync();
      return Ok(logs);
    }

    [Route("mine")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GetLogDto>>> GetMyLogs()
    {
      var logs = await logService.getMyLogsAsync(User);
      return Ok(logs);
    }
  }
}
