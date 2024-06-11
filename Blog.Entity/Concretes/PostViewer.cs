using Blog.Core.Entities.Abstracts;

namespace Blog.Entity.Concretes;

public class PostViewer : BaseEntity
{
  public int AppUserId { get; set; }
  public virtual AppUser AppUser { get; set; }

  public int PostId { get; set; }
  public virtual Post Post { get; set; }
}