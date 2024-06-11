using Blog.Common.Attributes;
using Blog.Common.Dtos.Authentication;
using Blog.Service;
using Blog.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blog.WebApi.Controllers;

[ApiRoute]
public class AuthenticationController(IUserService userService) : BaseController
{
  [HttpPost("register")]
  [SwaggerOperation(
    Summary = "Registers a new user",
    OperationId = "Authentication.Register",
    Tags = new[] { "AuthenticationController" }
  )]
  public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
  {
    var res = await userService.RegisterAsync(dto);
    return HandleResponse(res);
  }

  [HttpPost("token")]
  [SwaggerOperation(
    Summary = "Provides a token for the user",
    OperationId = "Authentication.GetToken",
    Tags = new[] { "AuthenticationController" }
  )]
  public async Task<IActionResult> GetToken([FromBody] LoginDto dto)
  {
    var res = await userService.GetTokenAsync(dto);
    return HandleResponse(res, true);
  }
}