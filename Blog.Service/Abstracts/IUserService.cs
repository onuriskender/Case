using Blog.Common.Dtos;
using Blog.Common.Dtos.Authentication;

namespace Blog.Service.Abstracts;

public interface IUserService
{
  /// <summary>
  /// Asynchronously registers a new user with the given details.
  /// </summary>
  /// <param name="registerUserDto">The details of the user to be registered.</param>
  /// <returns>A response containing the result of the registration operation.</returns>
  Task<ResponseDto> RegisterAsync(RegisterUserDto registerUserDto);

  /// <summary>
  /// Asynchronously generates a token.
  /// </summary>
  /// <param name="loginDto">The login details of the user.</param>
  /// <returns>A response containing the generated token if the authentication is successful.</returns>
  Task<ResponseDto> GetTokenAsync(LoginDto loginDto);

  /// <summary>
  /// Asynchronously retrieves a user by their ID.
  /// </summary>
  /// <param name="id">The ID of the user to be retrieved.</param>
  /// <returns>A response containing the user details if found.</returns>
  Task<ResponseDto> GetUserByIdAsync(int id);
}