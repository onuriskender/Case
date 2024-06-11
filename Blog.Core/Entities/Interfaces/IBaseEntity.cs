namespace Blog.Core.Entities.Interfaces;

public interface IBaseEntity
{
  int Id { get; set; }
  bool IsActive { get; set; }
  bool IsDeleted { get; set; }
  DateTime CreatedDate { get; set; }
  DateTime UpdatedDate { get; set; }
}