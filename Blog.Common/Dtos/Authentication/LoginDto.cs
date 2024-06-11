using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Dtos.Authentication;

public class LoginDto
{
  [Required]
  public string Email { get; set; }
  
  [Required]
  public string Password { get; set; }
}