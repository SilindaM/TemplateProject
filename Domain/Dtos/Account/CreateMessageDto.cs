namespace Domain.Dtos.Account
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class CreateMessageDto
  {
    public string ReceiverUserName { get; set; }
    public string Text { get; set; }
  }
}
