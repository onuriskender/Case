using System.Net;
using Blog.Common.Dtos;
using Blog.Common.Dtos.Authentication;
using Blog.Service.Abstracts;
using Blog.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;

namespace Blog.Test;

public class AuthenticationControllerTests
{
  private readonly AuthenticationController _controller;
  private readonly Mock<IUserService> _userServiceMock;

  public AuthenticationControllerTests()
  {
    _userServiceMock = new Mock<IUserService>();
    _controller = new AuthenticationController(_userServiceMock.Object);
  }

  [Fact]
  public async Task Register_ReturnsOkResult_WhenRegistrationIsSuccessful()
  {
    // Arrange
    var dto = new RegisterUserDto { UserName = "testuser", Password = "password123", Email = "test@example.com", Name = "Test", LastName = "User" };
    var serviceResponse = new ResponseDto
    {
      StatusCode = HttpStatusCode.OK,
      Message = "User registered successfully.",
      Data = null
    };

    _userServiceMock.Setup(s => s.RegisterAsync(dto)).ReturnsAsync(serviceResponse);

    // Act
    var result = await _controller.Register(dto);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    var responseMessage = Assert.IsType<string>(okResult.Value);
    Assert.Equal("User registered successfully.", responseMessage);
  }

  [Fact]
  public async Task GetToken_ReturnsOkResult_WhenTokenIsProvided()
  {
    // Arrange
    var dto = new LoginDto { Email = "test@test.com", Password = "password123" };
    var serviceResponse = new ResponseDto
    {
      StatusCode = HttpStatusCode.OK,
      Message = "Token generated successfully.",
      Data = "example-token"
    };

    _userServiceMock.Setup(s => s.GetTokenAsync(dto)).ReturnsAsync(serviceResponse);

    // Act
    var result = await _controller.GetToken(dto);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    var responseJson = JObject.FromObject(okResult.Value);
        
    Assert.Equal("Token generated successfully.", responseJson["Message"].ToString());
    Assert.Equal("example-token", responseJson["Data"].ToString());
  }
}