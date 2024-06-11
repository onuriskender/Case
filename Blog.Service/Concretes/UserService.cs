using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Blog.Common.Const;
using Blog.Common.Dtos;
using Blog.Common.Dtos.Authentication;
using Blog.Common.Helpers;
using Blog.Entity.Concretes;
using Blog.Service.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Blog.Service.Concretes;

public class UserService : BaseService, IUserService
{
  public async Task<ResponseDto> RegisterAsync(RegisterUserDto registerUserDto)
  {
    try
    {
      if (registerUserDto is null)
      {
        res.StatusCode = HttpStatusCode.NotFound;
        res.Message = "Body is null";
        return res;
      }

      var propertiesToCheck = new Dictionary<Func<RegisterUserDto, string>, string>
      {
        { dto => dto.Email, "Email is required" },
        { dto => dto.Password, "Password is required" },
        { dto => dto.UserName, "UserName is required" },
        { dto => dto.Name, "Name is required" },
        { dto => dto.LastName, "LastName is required" }
      };

      var emptyProperties = new List<string>();

      foreach (var property in propertiesToCheck)
      {
        var value = property.Key(registerUserDto);
        if (string.IsNullOrWhiteSpace(value))
        {
          emptyProperties.Add(property.Value);
        }
      }

      if (emptyProperties.Any())
      {
        res.Message = string.Join('\n', emptyProperties);
        return res;
      }

      if (registerUserDto.Email.Length < 5 || !registerUserDto.Email.Contains('@') ||
          !registerUserDto.Email.Contains('.'))
      {
        res.Message = "Email is not valid";
        return res;
      }

      if (registerUserDto.Password.Trim().Length < 4)
      {
        res.Message = "Password must be at least 4 characters";
        return res;
      }

      var checkMail = await Services.UserManager.FindByEmailAsync(registerUserDto.Email);

      if (checkMail != null)
      {
        res.StatusCode = HttpStatusCode.Found;
        res.Message = "Email is already in use";
        return res;
      }

      var user = new AppUser
      {
        Name = registerUserDto.Name.Trim(),
        LastName = registerUserDto.LastName.Trim(),
        UserName = registerUserDto.UserName.Trim()
          .ToLower()
          .RemoveSpaces()
          .ConvertTurkishCharsToEnglish(),
        Email = registerUserDto.Email.Trim(),
        CreatedDate = DateTime.UtcNow,
        UpdatedDate = DateTime.UtcNow,
        IsActive = true
      };

      var result = await Services.UserManager.CreateAsync(user, registerUserDto.Password);

      if (result.Succeeded)
      {
        res.StatusCode = HttpStatusCode.Created;
        res.Message = "User created successfully";
      }
      else
        res.Message = "User could not be created";

      return res;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }

  public async Task<ResponseDto> GetTokenAsync(LoginDto loginDto)
  {
    try
    {
      if (loginDto is null)
      {
        res.StatusCode = HttpStatusCode.NotFound;
        res.Message = "Body is null";
        return res;
      }
      
      var user = await Services.UserManager.FindByEmailAsync(loginDto.Email);
      if (user == null)
      {
        res.StatusCode = HttpStatusCode.NotFound;
        res.Message = "User not found";
        return res;
      }

      var passCheck = await Services.UserManager.CheckPasswordAsync(user, loginDto.Password);

      if (!passCheck)
      {
        res.StatusCode = HttpStatusCode.Unauthorized;
        res.Message = "Invalid password";
        return res;
      }

      var token = this.GenerateJwtToken(loginDto.Email, user.Id.ToString());
      res.StatusCode = HttpStatusCode.OK;
      res.Message = "Token successfully created. If you are using Swagger, please add “Bearer” at the beginning of the token";
      res.Data = token;

      return res;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }

  private string GenerateJwtToken(string email, string userId)
  {
    var _configuration = HttpHelper.GetService<IConfiguration>();

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, email),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new Claim(Consts.UserIdClaim, userId)
    };

    var token = new JwtSecurityToken(
      issuer: _configuration["Jwt:Issuer"],
      audience: _configuration["Jwt:Audience"],
      claims: claims,
      expires: DateTime.Now.AddMinutes(30),
      signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
  
  public async Task<ResponseDto> GetUserByIdAsync(int id)
  {
    try
    {
      var user = await Services.UserManager.FindByIdAsync(id.ToString());
      if (user is null)
      {
        res.StatusCode = HttpStatusCode.NotFound;
        res.Message = "User not found";
        return res;
      }

      res.StatusCode = HttpStatusCode.OK;
      res.Message = "User found";
      res.Data = user;

      return res;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }
}