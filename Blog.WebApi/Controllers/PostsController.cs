using Blog.Common.Attributes;
using Blog.Common.Dtos.Post;
using Blog.Service.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

[ApiRoute]
[ApiController]
public class PostsController(IPostService postService) : BaseController
{
  /// <summary>
  /// Gets all active posts.
  /// </summary>
  /// <remarks>
  /// Sample request:
  ///
  ///     GET /api/posts
  ///
  /// </remarks>
  /// <returns>A list of active posts.</returns>
  /// <response code="200">Returns the list of active posts.</response>
  [HttpGet]
  public async Task<ActionResult<List<PostListDto>>> Get()
  {
    return await postService.GetActivePostsAsync();
  }

  /// <summary>
  /// Gets a specific post by ID.
  /// </summary>
  /// <remarks>
  /// Sample request:
  ///
  ///     GET /api/posts/5
  ///
  /// </remarks>
  /// <param name="id">The ID of the post to be retrieved.</param>
  /// <returns>A post with the specified ID.</returns>
  /// <response code="200">Returns the post with the specified ID.</response>
  /// <response code="400">If the ID equals to or less than zero.</response>
  /// <response code="404">If the post with the specified ID is not found.</response>
  [HttpGet("{id}")]
  public async Task<IActionResult> Get([FromRoute] int id)
  {
    if (User.Identity != null && User.Identity.IsAuthenticated)
      await postService.IncreaseViewCountAsync(id);

    var res = await postService.GetActivePostByIdAsync(id);
    return HandleResponse(res, true);
  }

  /// <summary>
  /// Creates a new post.
  /// </summary>
  /// <remarks>
  /// Sample request:
  ///
  ///     POST /api/posts
  ///     {
  ///        "title": "Post Title",
  ///        "content": "Post Content"
  ///     }
  ///
  /// </remarks>
  /// <param name="dto">The details of the post to be created.</param>
  /// <returns>A response containing the result of the creation operation.</returns>
  /// <response code="201">Returns the result of the creation operation.</response>
  /// <response code="400">If the post details are invalid.</response>
  /// <response code="404">If the body is null.</response>
  [HttpPost]
  [Authorize]
  public async Task<IActionResult> Post([FromBody] CreatePostDto dto)
  {
    var res = await postService.CreatePostAsync(dto);
    return HandleResponse(res);
  }

  /// <summary>
  /// Updates a specific post.
  /// </summary>
  /// <remarks>
  /// Sample request:
  ///
  ///     PATCH /api/posts/5
  ///     {
  ///        "title": "Updated Title",
  ///        "content": "Updated Content"
  ///     }
  ///
  /// </remarks>
  /// <param name="dto">The updated details of the post.</param>
  /// <param name="id">The ID of the post to be updated.</param>
  /// <returns>A response containing the result of the update operation.</returns>
  /// <response code="200">Returns the result of the update operation.</response>
  /// <response code="400">If the post with the specified ID is not found.</response>
  /// <response code="401">If the user is not the owner of the post.</response>
  /// <response code="404">If the post is not found.</response>
  [HttpPatch("{id}")]
  [Authorize]
  public async Task<IActionResult> Patch([FromBody] UpdatePostDto dto, [FromRoute] int id)
  {
    var res = await postService.UpdatePostAsync(dto, id);
    return HandleResponse(res);
  }

  /// <summary>
  /// Deletes a specific post.
  /// </summary>
  /// <remarks>
  /// Sample request:
  ///
  ///     DELETE /api/posts/5
  ///
  /// </remarks>
  /// <param name="id">The ID of the post to be deleted.</param>
  /// <returns>A response containing the result of the deletion operation.</returns>
  /// <response code="200">Returns the result of the deletion operation.</response>
  /// <response code="400">If the post with the specified ID is not found.</response>
  /// <response code="401">If the user is not the owner of the post.</response>
  /// <response code="404">If the post is not found.</response>
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<IActionResult> Delete([FromRoute] int id)
  {
    var res = await postService.DeletePostAsync(id);
    return HandleResponse(res);
  }
}