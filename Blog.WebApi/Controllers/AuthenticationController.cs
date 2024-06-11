using Blog.Common.Attributes;
using Blog.Common.Dtos.Authentication;
using Blog.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiRoute]
public class AuthenticationController(IUserService userService) : BaseController
{
  /// <summary>
  /// Registers a new user.
  /// </summary>
  /// <remarks>
  /// Sample request:
  ///
  ///     POST /api/authentication/register
  ///     {
  ///        "email": "user@example.com",
  ///        "password": "password123"
  ///     }
  ///
  /// </remarks>
  /// <param name="dto">The registration details of the user.</param>
  /// <returns>A response containing the result of the registration operation.</returns>
  /// <response code="200">Returns the result of the registration operation.</response>
  /// <response code="400">If the registration details are invalid.</response>
  /// <response code="404">If the body is empty.</response>
  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
  {
    var res = await userService.RegisterAsync(dto);
    return HandleResponse(res);
  }

  /// <summary>
  /// Provides a token for the user.
  /// </summary>
  /// <remarks>
  /// Sample request:
  ///
  ///     POST /api/authentication/token
  ///     {
  ///        "email": "user@example.com",
  ///        "password": "password123"
  ///     }
  ///
  /// </remarks>
  /// <param name="dto">The login details of the user.</param>
  /// <returns>A response containing the generated token if the authentication is successful.</returns>
  /// <response code="200">Returns the generated token if the authentication is successful.</response>
  /// <response code="401">If the password is invalid.</response>
  /// <response code="404">If the body is null or user cannot found in db.</response>
  [HttpPost("token")]
  public async Task<IActionResult> GetToken([FromBody] LoginDto dto)
  {
    var res = await userService.GetTokenAsync(dto);
    return HandleResponse(res, true);
  }
}