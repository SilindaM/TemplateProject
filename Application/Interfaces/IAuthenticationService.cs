namespace Application.Interfaces
{
  using Domain.Dtos;
  using FluentResults;

public interface IAuthenticationService
{
  Task<Result<string>> Register(RegisterRequest request);
  Task<Result<string>> Login(LoginRequest request);
}
}