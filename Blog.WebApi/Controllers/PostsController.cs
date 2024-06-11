using Blog.Common.Attributes;
using Blog.Common.Dtos.Post;
using Blog.Service.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blog.WebApi.Controllers;

[ApiRoute]
[ApiController]
public class PostsController(IPostService postService) : BaseController
{
  
  
  [HttpGet]
  [SwaggerOperation(
    Summary = "Gets all active posts",
    OperationId = "Post.GetAll",
    Tags = new[] { "PostsController" }
  )]
  public async Task<ActionResult<List<PostListDto>>> Get()
  {
    return await postService.GetActivePostsAsync();
  }

  [HttpGet("{id}")]
  [SwaggerOperation(
    Summary = "Gets a specific post by ID",
    OperationId = "Post.Get",
    Tags = new[] { "PostsController" }
  )]
  public async Task<IActionResult> Get([FromRoute] int id)
  {
    if (User.Identity != null && User.Identity.IsAuthenticated)
      await postService.IncreaseViewCountAsync(id);

    var res = await postService.GetActivePostByIdAsync(id);
    return HandleResponse(res, true);
  }

  [HttpPost]
  [Authorize]
  [SwaggerOperation(
    Summary = "Creates a new post",
    Description = "Requires authentication",
    OperationId = "Post.Create",
    Tags = new[] { "PostsController" }
  )]
  public async Task<IActionResult> Post([FromBody] CreatePostDto dto)
  {
    var res = await postService.CreatePostAsync(dto);
    return HandleResponse(res);
  }

  [HttpPatch("{id}")]
  [Authorize]
  [SwaggerOperation(
    Summary = "Updates a specific post",
    Description = "Requires authentication",
    OperationId = "Post.Update",
    Tags = new[] { "PostsController" }
  )]
  public async Task<IActionResult> Patch([FromBody] UpdatePostDto dto, [FromRoute] int id)
  {
    var res = await postService.UpdatePostAsync(dto, id);
    return HandleResponse(res);
  }

  [HttpDelete("{id}")]
  [Authorize]
  [SwaggerOperation(
    Summary = "Deletes a specific post",
    Description = "Requires authentication",
    OperationId = "Post.Delete",
    Tags = new[] { "PostsController" }
  )]
  public async Task<IActionResult> Delete([FromRoute] int id)
  {
    var res = await postService.DeletePostAsync(id);
    return HandleResponse(res);
  }
}