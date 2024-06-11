using Blog.Core.Entities.Abstracts;

namespace Blog.Entity.Concretes;

public class Post : BaseEntity
{
  public required string Title { get; set; }
  public required string Content { get; set; }
  public int ViewCount { get; set; }
  public int AppUserId { get; set; }
  public virtual AppUser AppUser { get; set; }
}