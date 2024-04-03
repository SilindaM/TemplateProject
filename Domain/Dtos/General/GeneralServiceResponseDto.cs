namespace Domain.Dtos.General
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class GeneralServiceResponseDto
  {
    public bool IsSucceed { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
  }
}
