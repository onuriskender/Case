using System.Net;
using Blog.Common.Dtos;
using Blog.Common.Dtos.Post;
using Blog.Common.Helpers;
using Blog.Entity.Concretes;
using Blog.Service.Abstracts;

namespace Blog.Service.Concretes;

public class PostService : BaseService, IPostService
{
  public async Task<List<PostListDto>> GetActivePostsAsync()
  {
    try
    {
      var posts = await Repositories.PostRepository
        .ToListAsync(x => x.IsActive
                          && !x.IsDeleted);

      if (!posts.Any())
        return new List<PostListDto>();

      return posts.Select(x => new PostListDto
        {
          Id = x.Id,
          CreatedDate = x.CreatedDate
            .ConvertUtcToIstanbul(),
          Title = x.Title
        })
        .OrderBy(x => x.CreatedDate)
        .ToList();
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }

  public async Task<ResponseDto> GetActivePostByIdAsync(int id)
  {
    try
    {
      if (id <= 0)
      {
        res.Message = "Id not found";
        return res;
      }

      var post = await Repositories.PostRepository.FirstOrDefaultAsync(x => x.IsActive
                                                                            && !x.IsDeleted
                                                                            && x.Id == id);

      if (post is null)
      {
        res.StatusCode = HttpStatusCode.NotFound;
        res.Message = "Post not found";
        return res;
      }

      res.StatusCode = HttpStatusCode.OK;
      res.Message = "Post successfully fetched";

      var authorUser = await Services.UserService.GetUserByIdAsync(post.AppUserId);

      res.Data = new PostDetailDto
      {
        Id = post.Id,
        Title = post.Title,
        Content = post.Content,
        CreatedDate = post.CreatedDate.ConvertUtcToIstanbul(),
        ViewCount = post.ViewCount,
        Author = authorUser.StatusCode is HttpStatusCode.OK
          ? $"{((AppUser)authorUser.Data).Name} {((AppUser)authorUser.Data).LastName}"
          : string.Empty
      };

      return res;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }

  public async Task IncreaseViewCountAsync(int postId)
  {
    try
    {
      if (postId <= 0)
        return;

      var isUserViewed = await Services.PostViewerService.CheckToUserViewedPostAsync(postId);

      if (!isUserViewed)
      {
        var post = await Repositories.PostRepository.FirstOrDefaultAsync(x => x.IsActive
                                                                              && !x.IsDeleted
                                                                              && x.Id == postId);

        if (post is null)
          return;

        var result = await Services.PostViewerService.InsertPostViewerAsync(postId);

        if (result.StatusCode == HttpStatusCode.Created)
        {
          post.ViewCount = (int)result.Data;

          await Repositories.PostRepository.UpdateAsync(post);
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }

  public async Task<ResponseDto> CreatePostAsync(CreatePostDto dto)
  {
    try
    {
      if (dto is null)
      {
        res.StatusCode = HttpStatusCode.NotFound;
        res.Message = "Body is null";
        return res;
      }

      var propertiesToCheck = new Dictionary<Func<CreatePostDto, string>, string>
      {
        { dto => dto.Title, "Title is required" },
        { dto => dto.Content, "Content is required" }
      };

      var emptyProperties = new List<string>();

      foreach (var property in propertiesToCheck)
      {
        var value = property.Key(dto);
        if (string.IsNullOrWhiteSpace(value))
        {
          emptyProperties.Add(property.Value);
        }
      }

      if (emptyProperties.Any())
      {
        res.Message = string.Join('\n', emptyProperties);
        return res;
      }

      var post = new Post
      {
        Title = dto.Title.Trim(),
        Content = dto.Content.Trim(),
        AppUserId = UserId.Value
      };

      var result = await Repositories.PostRepository.AddAsync(post);

      if (result > 0)
      {
        res.StatusCode = HttpStatusCode.Created;
        res.Message = "Post created successfully";
      }
      else
        res.Message = "Post could not be created";

      return res;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }

  public async Task<ResponseDto> UpdatePostAsync(UpdatePostDto dto, int id)
  {
    try
    {
      if (id <= 0)
      {
        res.Message = "Id not found";
        return res;
      }

      var post = await Repositories.PostRepository.FirstOrDefaultAsync(x => x.IsActive
                                                                            && !x.IsDeleted
                                                                            && x.Id == id);

      if (post is null)
      {
        res.StatusCode = HttpStatusCode.NotFound;
        res.Message = "Post not found";
        return res;
      }

      if (post.AppUserId != UserId)
      {
        res.StatusCode = HttpStatusCode.Unauthorized;
        res.Message = "You are not authorized to make changes";
        return res;
      }

      if (!string.IsNullOrWhiteSpace(dto.Title))
      {
        post.Title = dto.Title.Trim();
      }

      if (!string.IsNullOrWhiteSpace(dto.Content))
      {
        post.Content = dto.Content.Trim();
      }

      var result = await Repositories.PostRepository.UpdateAsync(post);

      if (result > 0)
      {
        res.StatusCode = HttpStatusCode.OK;
        res.Message = "Post updated successfully";
      }
      else
        res.Message = "Post could not be updated";

      return res;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }

  public async Task<ResponseDto> DeletePostAsync(int id)
  {
    try
    {
      if (id <= 0)
      {
        res.Message = "Id not found";
        return res;
      }

      var post = await Repositories.PostRepository.FirstOrDefaultAsync(x => x.IsActive
                                                                            && !x.IsDeleted
                                                                            && x.Id == id);

      if (post is null)
      {
        res.StatusCode = HttpStatusCode.NotFound;
        res.Message = "Post not found";
        return res;
      }

      if (post.AppUserId != UserId)
      {
        res.StatusCode = HttpStatusCode.Unauthorized;
        res.Message = "You are not authorized to make changes";
        return res;
      }

      var result = await Repositories.PostRepository.DeleteAsync(post);

      if (result > 0)
      {
        res.StatusCode = HttpStatusCode.OK;
        res.Message = "Post deleted successfully";
      }
      else
        res.Message = "Post could not be deleted";

      return res;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }
}