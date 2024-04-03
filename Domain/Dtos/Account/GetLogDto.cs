namespace Domain.Dtos.Account
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class GetLogDto
  {
    public DateTime CreatedAt { get; set; }= DateTime.Now;
    public string? UserName { get; set; }
    public string Description { get; set; }
  }
}
