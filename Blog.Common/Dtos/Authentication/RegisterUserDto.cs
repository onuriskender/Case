using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Dtos.Authentication;

public class RegisterUserDto
{
  [Required]
  public string Email { get; set; }
  
  [Required]
  public string UserName { get; set; }
  
  [Required]
  public string Name { get; set; }
  
  [Required]
  public string LastName { get; set; }
  
  [Required]
  public string Password { get; set; }
}