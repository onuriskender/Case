using System.Net;
using Blog.Common.Dtos;
using Blog.Entity.Concretes;
using Blog.Service.Abstracts;

namespace Blog.Service.Concretes;

public class PostViewerService : BaseService, IPostViewerService
{
  public async Task<bool> CheckToUserViewedPostAsync(int postId)
  {
    try
    {
      return await Repositories.PostViewerRepository.AnyAsync(x => x.IsActive
                                                                   && !x.IsDeleted
                                                                   && x.PostId == postId
                                                                   && x.AppUserId == UserId);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }

  public async Task<ResponseDto> InsertPostViewerAsync(int postId)
  {
    try
    {
      if (!UserId.HasValue)
      {
        res.Message = "User not found";
        return res;
      }
        
      var postViewer = new PostViewer
      {
        PostId = postId,
        AppUserId = UserId.Value
      };

      var result = await Repositories.PostViewerRepository.AddAsync(postViewer);

      if (result > 0)
      {
        res.StatusCode = HttpStatusCode.Created;
        res.Message = "PostViewer created successfully";
        res.Data = await Repositories.PostViewerRepository.CountAsync(x => x.IsActive
                                                                           && !x.IsDeleted
                                                                           && x.PostId == postId);
      }
      else
        res.Message = "PostViewer could not be created";

      return res;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }
}