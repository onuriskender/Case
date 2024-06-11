using Blog.Core.Entities.Interfaces;

namespace Blog.Core.Entities.Abstracts;

public abstract class BaseEntity : IBaseEntity
{
  public int Id { get; set; }
  public bool IsActive { get; set; }
  public bool IsDeleted { get; set; }
  public DateTime CreatedDate { get; set; }
  public DateTime UpdatedDate { get; set; }
}