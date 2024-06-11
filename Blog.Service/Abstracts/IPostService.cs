using Blog.Common.Dtos;
using Blog.Common.Dtos.Post;

namespace Blog.Service.Abstracts;

public interface IPostService
{
  /// <summary>
  /// TODO:Onur summary ekleyecek
  /// </summary>
  /// <returns></returns>
  Task<List<PostListDto>> GetActivePostsAsync();

  /// <summary>
  /// TODO: Onur
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<ResponseDto> GetActivePostByIdAsync(int id);

  /// <summary>
  /// TODO: summary
  /// </summary>
  /// <param name="postId"></param>
  /// <returns></returns>
  Task IncreaseViewCountAsync(int postId);

  /// <summary>
  /// TODO: Summary
  /// </summary>
  /// <param name="dto"></param>
  /// <returns></returns>
  Task<ResponseDto> CreatePostAsync(CreatePostDto dto);

  /// <summary>
  /// TODO: summary
  /// </summary>
  /// <param name="dto"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<ResponseDto> UpdatePostAsync(UpdatePostDto dto, int id);

  /// <summary>
  /// TODO: summary
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<ResponseDto> DeletePostAsync(int id);
}