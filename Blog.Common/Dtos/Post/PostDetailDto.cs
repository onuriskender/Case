namespace Blog.Common.Dtos.Post;

public class PostDetailDto : BaseDto
{
  public string Title { get; set; }
  public string Content { get; set; }
  public int ViewCount { get; set; }
  public string Author { get; set; }
}