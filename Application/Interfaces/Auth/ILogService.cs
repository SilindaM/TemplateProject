namespace Application.Interfaces.Auth
{
  using Domain.Dtos.Account;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using System.Threading.Tasks;

  public interface ILogService
  {
    Task SaveNewLog(string UserName, string Description);
    Task<IEnumerable<GetLogDto>> getLogsAsync();
    Task<IEnumerable<GetLogDto>> getMyLogsAsync(ClaimsPrincipal User);
  }
}
