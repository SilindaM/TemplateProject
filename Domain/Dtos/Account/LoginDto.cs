using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Account
{
  public class LoginDto
  {
    [Required(ErrorMessage = "Username Required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password Required")]
    public string Password { get; set; }
  }
}
