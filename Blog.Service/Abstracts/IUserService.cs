using Blog.Common.Dtos;
using Blog.Common.Dtos.Authentication;

namespace Blog.Service.Abstracts;

public interface IUserService
{
  /// <summary>
  /// TODO: RegisterAsync
  /// </summary>
  /// <param name="registerUserDto"></param>
  /// <returns></returns>
  Task<ResponseDto> RegisterAsync(RegisterUserDto registerUserDto);
  
  /// <summary>
  /// TODO: LoginAsync
  /// </summary>
  /// <param name="loginDto"></param>
  /// <returns></returns>
  Task<ResponseDto> GetTokenAsync(LoginDto loginDto);

  /// <summary>
  /// TODO: onur
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<ResponseDto> GetUserByIdAsync(int id);
}