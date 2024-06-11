using System.Net;
using System.Security.Claims;
using Blog.Common.Const;
using Blog.Common.Dtos;
using Blog.Common.Dtos.Post;
using Blog.Service.Abstracts;
using Blog.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;

namespace Blog.Test;

public class PostsControllerTests
{
  private readonly PostsController _controller;
  private readonly Mock<IPostService> _postServiceMock;

  public PostsControllerTests()
  {
    _postServiceMock = new Mock<IPostService>();
    Mock<IHttpContextAccessor> httpContextAccessorMock = new();

    // Create a fake user and assign it to the context
    var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
    {
      new Claim(Consts.UserIdClaim, "7")
    }, "mock"));

    var httpContext = new DefaultHttpContext
    {
      User = user
    };

    httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

    // Instantiate the controller with mocked services
    _controller = new PostsController(_postServiceMock.Object)
    {
      ControllerContext = new ControllerContext
      {
        HttpContext = httpContext
      }
    };
  }

  [Fact]
  public async Task Get_ReturnsAllActivePosts()
  {
    // Arrange
    var posts = new List<PostListDto> { new PostListDto { Id = 1, Title = "Test Post" } };
    _postServiceMock.Setup(s => s.GetActivePostsAsync()).ReturnsAsync(posts);

    // Act
    var result = await _controller.Get();

    // Assert
    var okResult = Assert.IsType<ActionResult<List<PostListDto>>>(result);
    var returnValue = Assert.IsType<List<PostListDto>>(okResult.Value);
    Assert.Single(returnValue);
    Assert.Equal(1, returnValue[0].Id);
    Assert.Equal("Test Post", returnValue[0].Title);
  }

  [Fact]
  public async Task Get_ReturnsPostById_WhenPostExists()
  {
    // Arrange
    var postId = 1;
    var postResponse = new ResponseDto
    {
      StatusCode = HttpStatusCode.OK,
      Message = "Post found",
      Data = new PostDetailDto { Id = postId, Title = "Test Post" }
    };

    _postServiceMock.Setup(s => s.GetActivePostByIdAsync(postId)).ReturnsAsync(postResponse);

    // Act
    var result = await _controller.Get(postId);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    var responseJson = JObject.FromObject(okResult.Value);

    Assert.Equal("Post found", responseJson["Message"].ToString());
    Assert.Equal(postId, (int)responseJson["Data"]["Id"]);
  }

  [Fact]
  public async Task Post_CreatesNewPost()
  {
    // Arrange
    var createPostDto = new CreatePostDto { Title = "New Post", Content = "This is a new post." };
    var serviceResponse = new ResponseDto
    {
      StatusCode = HttpStatusCode.OK,
      Message = "Post created successfully.",
      Data = null
    };

    _postServiceMock.Setup(s => s.CreatePostAsync(createPostDto)).ReturnsAsync(serviceResponse);

    // Act
    var result = await _controller.Post(createPostDto);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    var responseMessage = Assert.IsType<string>(okResult.Value);
    Assert.Equal("Post created successfully.", responseMessage);
  }

  [Fact]
  public async Task Patch_UpdatesPost()
  {
    // Arrange
    var updatePostDto = new UpdatePostDto { Title = "Updated Post", Content = "Updated content." };
    var postId = 1;
    var serviceResponse = new ResponseDto
    {
      StatusCode = HttpStatusCode.OK,
      Message = "Post updated successfully.",
      Data = null
    };

    _postServiceMock.Setup(s => s.UpdatePostAsync(updatePostDto, postId)).ReturnsAsync(serviceResponse);

    // Act
    var result = await _controller.Patch(updatePostDto, postId);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    var responseMessage = Assert.IsType<string>(okResult.Value);
    Assert.Equal("Post updated successfully.", responseMessage);
  }

  [Fact]
  public async Task Delete_DeletesPost()
  {
    // Arrange
    var postId = 1;
    var serviceResponse = new ResponseDto
    {
      StatusCode = HttpStatusCode.OK,
      Message = "Post deleted successfully.",
      Data = null
    };

    _postServiceMock.Setup(s => s.DeletePostAsync(postId)).ReturnsAsync(serviceResponse);

    // Act
    var result = await _controller.Delete(postId);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    var responseMessage = Assert.IsType<string>(okResult.Value);
    Assert.Equal("Post deleted successfully.", responseMessage);
  }
}