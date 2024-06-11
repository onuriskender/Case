using System.ComponentModel.DataAnnotations.Schema;
using Blog.Core.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Blog.Entity.Concretes;

[Table("AppUsers")]
public class AppUser : IdentityUser<int>, IBaseEntity
{
  public required string Name { get; set; }
  public required string LastName { get; set; }
  public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
  public bool IsActive { get; set; }
  public bool IsDeleted { get; set; }
  public DateTime CreatedDate { get; set; }
  public DateTime UpdatedDate { get; set; }
}