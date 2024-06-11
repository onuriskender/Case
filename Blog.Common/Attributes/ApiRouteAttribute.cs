using Microsoft.AspNetCore.Mvc;

namespace Blog.Common.Attributes;

public class ApiRouteAttribute : RouteAttribute
{
  public ApiRouteAttribute() : base("api/[controller]")
  {
  }
}