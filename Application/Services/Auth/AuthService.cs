namespace Application.Services.Auth
{
  using Application.Helpers;
  using Application.Interfaces.Auth;
  using Domain.Account;
  using Domain.Constants;
  using Domain.Dtos.Account;
  using Domain.Dtos.General;
  using Domain.Enums;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.Options;
  using Microsoft.IdentityModel.Tokens;
  using System;
  using System.Collections.Generic;
  using System.IdentityModel.Tokens.Jwt;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using System.Threading.Tasks;

  public class AuthService : IAuthService
  {
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IConfiguration configuration;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly ILogService logService;

    public AuthService(UserManager<ApplicationUser> userManager,IConfiguration configuration, RoleManager<IdentityRole> roleManager, ILogService logService)
    {
      this.userManager = userManager;
      this.configuration = configuration;
      this.roleManager = roleManager;
      this.logService = logService;
    }

    public async Task<UserInfoResult> GetUserDetailsByUserNamesync(string userName)
    {
      var user = await userManager.FindByNameAsync(userName);
      if(user is null ) { return null; }
      
      var roles = await userManager.GetRolesAsync(user);
      var userInfor = GenerateUserInfoObject(user, roles);
      return (UserInfoResult)userInfor;
    }

    public async Task<IEnumerable<UserInfoResult>> GetUserListAsync()
    {
      var users = await userManager.Users.ToListAsync();
      List<UserInfoResult> userInfoResults = new List<UserInfoResult>(); 

      foreach(var user in users)
      {
        var roles = await userManager.GetRolesAsync(user);
        var userInfo = GenerateUserInfoObject(user, roles);
        userInfoResults.Add((UserInfoResult)userInfo);
      }
      return userInfoResults;
    }

    public async Task<IEnumerable<string>> GetUsernamesListAsync()
    {
     return await userManager.Users.Select(u=>u.UserName).ToListAsync();
    }

    public async Task<LoginServiceResponseDto> LoginAsync(LoginDto loginDto)
    {
      var user = await userManager.FindByEmailAsync(loginDto.Username);
      if(user is null)
      {
        return null;
      }
      var isPasswordCorrect = await userManager.CheckPasswordAsync(user, loginDto.Password);
      if(!isPasswordCorrect)
      {
        return null;
      }
      var newToken = await GenerateJWTTokenAsync(user);
      var roles = await userManager.GetRolesAsync(user);
      var userInfo = GenerateUserInfoObject(user,roles);

      await logService.SaveNewLog(user.UserName, "New Login");
      return new LoginServiceResponseDto { NewToken = newToken ,UserInfo = (UserInfoResult)userInfo };
    }

  
    public async Task<LoginServiceResponseDto> MeAsync(MeDto meDto)
    {
      ClaimsPrincipal handler = new JwtSecurityTokenHandler().ValidateToken(meDto.Token, new TokenValidationParameters()
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidAudience = configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
      },out SecurityToken securityToken);

      string decodeUsername = handler.Claims.First(x => x.Type == ClaimTypes.Name).Value;
      if(decodeUsername is null) { return null; }
     
      var user = await userManager.FindByNameAsync(decodeUsername);
      if(user is null) { return null; }

      var newToken = await GenerateJWTTokenAsync(user);
      var roles = await userManager.GetRolesAsync(user);
      var userInfo = GenerateUserInfoObject(user, roles);
      await logService.SaveNewLog(user.UserName, "New Token Generated");

      return new LoginServiceResponseDto { NewToken = newToken, UserInfo = (UserInfoResult)userInfo };
    }

    public async Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto registerDto)
    {
      var userExists = await userManager.FindByEmailAsync(registerDto.Email);
      if(userExists is not null) 
      {
        ResponseHelper.CreateResponse(false, 409, "User Already Exist");
      }
      ApplicationUser user = new ApplicationUser()
      {
        Email = registerDto.Email,
        FirstName = registerDto.FirstName,
        LastName = registerDto.LastName,
        Address = registerDto.Address,
        UserName = registerDto.Username,
        SecurityStamp = Guid.NewGuid().ToString(),
      };
      var createUserResult = await userManager.CreateAsync(user,registerDto.Password);
      if(!createUserResult.Succeeded) 
      {
        var errorString = "Failed to create user";
        foreach(var error in createUserResult.Errors)
        {
          errorString += " # " + error.Description;
        }
        return ResponseHelper.CreateResponse(false, 400, errorString);
      }
      await userManager.AddToRoleAsync(user,StaticUserRoles.USER);
      await logService.SaveNewLog(user.UserName, "Registed");
      return ResponseHelper.CreateResponse(true, 201, "User Registed Successfully");
    }

    public async Task<GeneralServiceResponseDto> SeedRolesAsync()
    {
      string[] rolesToSeed = { StaticUserRoles.OWNER, StaticUserRoles.ADMIN, StaticUserRoles.MANAGER, StaticUserRoles.USER };

      bool[] rolesExist = await Task.WhenAll(rolesToSeed.Select(role => roleManager.RoleExistsAsync(role)));

      if (rolesExist.All(x => x))
      {
        return ResponseHelper.CreateResponse(true, 200, "Roles Seeding Already Done");
      }

      for (int i = 0; i < rolesToSeed.Length; i++)
      {
        if (!rolesExist[i])
        {
          await roleManager.CreateAsync(new IdentityRole(rolesToSeed[i]));
        }
      }

      return ResponseHelper.CreateResponse(true, 200, "Roles Seeding Successful");
    }


    public async Task<GeneralServiceResponseDto> UpdateRoleAsync(ClaimsPrincipal User, UpdateRoleDto updateRoleDto)
    {
      var user = await userManager.FindByNameAsync(updateRoleDto.Username);
      if(user is null)
      {
        return ResponseHelper.CreateResponse(false, 404, "Invalid UserName");
      }
      var userRoles = await userManager.GetRolesAsync(user);
      //admin and owner can change
      if(User.IsInRole(StaticUserRoles.ADMIN))
      {
        //user is admin
        if (updateRoleDto.NewRole == RoleType.USER || updateRoleDto.NewRole == RoleType.MANAGER)
        {
          //admin can change role for everyone except for owners and admins
          if (userRoles.Any(r => r.Equals(StaticUserRoles.OWNER) || r.Equals(StaticUserRoles.OWNER)))
          {
            return ResponseHelper.CreateResponse(false, 403, "You not allowed to change role for this user");
          }
          else
          {
            await userManager.RemoveFromRolesAsync(user, userRoles);
            await userManager.AddToRoleAsync(user, updateRoleDto.NewRole.ToString());
            await logService.SaveNewLog(user.UserName, "Role update");
            return ResponseHelper.CreateResponse(true, 200, "Role Updated Successfully");
          }
        }
        else return ResponseHelper.CreateResponse(false, 403, "You are not allowed to change role of this user");
      }
      //USER IS THE OWNER
      else
      {
        if (userRoles.Any(x => x.Equals(StaticUserRoles.OWNER)))
        {
          return ResponseHelper.CreateResponse(false, 403, "You are not allowed to change role of this user");
        }
        else
        {
          await userManager.RemoveFromRolesAsync(user, userRoles);
          await userManager.AddToRoleAsync(user, updateRoleDto.NewRole.ToString());
          await logService.SaveNewLog(user.UserName, "Role update");
          return ResponseHelper.CreateResponse(true, 200, "Role Updated Successfully");
        }
      }
    }
    private object GenerateUserInfoObject(ApplicationUser user, IList<string> roles)
    {
      return new UserInfoResult
      {
        Id = user.Id,
        CreatedAt = DateTime.Now,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Roles = roles,
        Username = user.UserName,
      };
    }

    private async Task<string> GenerateJWTTokenAsync(ApplicationUser applicationUser)
    {
      var userRoles = await userManager.GetRolesAsync(applicationUser);
      var authClaims = new List<Claim>()
      {
        new Claim(ClaimTypes.Name,applicationUser.UserName),
        new Claim(ClaimTypes.NameIdentifier,applicationUser.Id),
        new Claim("FirstName",applicationUser.UserName),
        new Claim("LastName",applicationUser.UserName),
      };
      foreach(var role in userRoles)
      {
        authClaims.Add(new Claim(ClaimTypes.Role,role));
      }
      var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
      var signinCredentials = new SigningCredentials(authSecret,SecurityAlgorithms.HmacSha256);

      var tokenObject = new JwtSecurityToken(
        issuer: configuration["JWT:ValidIssuer"],
        audience: configuration["JWT:ValidAudience"],
        notBefore: DateTime.Now,
        expires: DateTime.Now.AddHours(9),
        claims: authClaims,
        signingCredentials: signinCredentials);

      string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
      return token;
    }
  }
}
