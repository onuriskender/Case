using Blog.Common.Dtos;
using Blog.Common.Dtos.Post;

namespace Blog.Service.Abstracts;

public interface IPostService
{
  /// <summary>
  /// Asynchronously retrieves a list of active posts.
  /// </summary>
  /// <returns>A list of active posts.</returns>
  Task<List<PostListDto>> GetActivePostsAsync();

  /// <summary>
  /// Asynchronously retrieves an active post by its ID.
  /// </summary>
  /// <param name="id">The ID of the post to be retrieved.</param>
  /// <returns>A response containing the post details if found.</returns>
  Task<ResponseDto> GetActivePostByIdAsync(int id);

  /// <summary>
  /// Asynchronously increases the view count of a post.
  /// </summary>
  /// <param name="postId">The ID of the post whose view count is to be increased.</param>
  Task IncreaseViewCountAsync(int postId);

  /// <summary>
  /// Asynchronously creates a new post with the given details.
  /// </summary>
  /// <param name="dto">The details of the post to be created.</param>
  /// <returns>A response containing the result of the creation operation.</returns>
  Task<ResponseDto> CreatePostAsync(CreatePostDto dto);

  /// <summary>
  /// Asynchronously updates a post with the given details.
  /// </summary>
  /// <param name="dto">The new details of the post.</param>
  /// <param name="id">The ID of the post to be updated.</param>
  /// <returns>A response containing the result of the update operation.</returns>
  Task<ResponseDto> UpdatePostAsync(UpdatePostDto dto, int id);

  /// <summary>
  /// Asynchronously deletes a post by its ID (soft delete).
  /// </summary>
  /// <param name="id">The ID of the post to be deleted.</param>
  /// <returns>A response containing the result of the deletion operation.</returns>
  Task<ResponseDto> DeletePostAsync(int id);
}