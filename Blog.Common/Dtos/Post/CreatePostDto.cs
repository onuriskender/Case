using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Dtos.Post;

public class CreatePostDto
{
  [Required]
  public string Title { get; set; }
  
  [Required]
  public string Content { get; set; }
}