using Blog.Common.Dtos;

namespace Blog.Service.Abstracts;

public interface IPostViewerService
{
  /// <summary>
  /// TODO: summary
  /// </summary>
  /// <param name="postId"></param>
  /// <returns></returns>
  Task<bool> CheckToUserViewedPostAsync(int postId);

  /// <summary>
  /// TODO: sum
  /// </summary>
  /// <param name="postId"></param>
  /// <returns></returns>
  Task<ResponseDto> InsertPostViewerAsync(int postId);
}