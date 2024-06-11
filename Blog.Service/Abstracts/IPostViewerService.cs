using Blog.Common.Dtos;

namespace Blog.Service.Abstracts;

public interface IPostViewerService
{
  /// <summary>
  /// Asynchronously checks if the user has viewed the post with the given ID.
  /// </summary>
  /// <param name="postId">The ID of the post to check.</param>
  /// <returns>True if the user has viewed the post; otherwise, false.</returns>
  Task<bool> CheckToUserViewedPostAsync(int postId);

  /// <summary>
  /// Asynchronously inserts a record indicating that the user has viewed the post with the given ID.
  /// </summary>
  /// <param name="postId">The ID of the post that the user has viewed.</param>
  /// <returns>A response containing the result of the insertion operation.</returns>
  Task<ResponseDto> InsertPostViewerAsync(int postId);
}