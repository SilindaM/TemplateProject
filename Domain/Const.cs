using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
  public static class Consts
  {
    public const int UsernameMinLength = 5;
        public const string PasswordRegex = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[\W_]).{8,}$";


        public const string UsernameLengthValidationError = "Username must have more than 5 characters.";
    public const string EmailValidationError = "Email must have valid format.";

    public const string PasswordValidationError = "Password must have more than 6 characters, min. 1 uppercase, min. 1 lowercase, min. 1 special characters.";
  }
}