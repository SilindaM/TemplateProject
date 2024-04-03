namespace API.Controllers.Auth
{
  using Application.Interfaces.Auth;
  using Application.Services.Auth;
  using Domain.Constants;
  using Domain.Dtos.Account;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;

  [Route("api/[controller]")]
  [ApiController]
  public class MessageController : ControllerBase
  {
    private readonly IMessageService messageService;

    public MessageController(IMessageService messageService)
    {
      this.messageService = messageService;
    }
    [HttpPost]
    [Authorize]
    [Route("Create")]
    public async Task<IActionResult> CreateNewMessage([FromBody] CreateMessageDto createMessageDto)
    {
      var result = await messageService.CreateNewMessageAsync(User, createMessageDto);
      if (result.IsSucceed)
      {
        return Ok(result.Message);
      }
      return StatusCode(result.StatusCode, result.Message);
    }
    [HttpGet]
    [Route("mine")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<GetMessageDto>>> GetMyMessage()
    {
      var messages = await messageService.GetMyMessageAsync(User);
      return Ok(messages);
    }
    [HttpGet]
    [Authorize(Roles = StaticUserRoles.OwnerAdmin)]
    public async Task<ActionResult<IEnumerable<GetMessageDto>>> GetMessages()
    {
      var messages = await messageService.GetMessagesAsync();
      return Ok(messages);
    }
  }
}