namespace Application.Helpers
{
  using Domain.Dtos.General;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class ResponseHelper
  {
    public static GeneralServiceResponseDto CreateResponse(bool isSucceed, int statusCode, string message)
    {
      return new GeneralServiceResponseDto()
      {
        IsSucceed = isSucceed,
        StatusCode = statusCode,
        Message = message
      };
    }
  }
}
