using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Account
{
  public class RegisterDto
  {
    [Required(ErrorMessage = "Username Required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "FirstName Required")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "LastName Required")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Email Required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password Required")]
    public string Password { get; set; }
  }
}