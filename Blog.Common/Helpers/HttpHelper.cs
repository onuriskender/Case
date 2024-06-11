using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Common.Helpers;

public static class HttpHelper
{
  public static IHttpContextAccessor _accessor;
  public static IServiceCollection _services;

  public static void Configure(IHttpContextAccessor accessor, IServiceCollection services)
  {
    _accessor = accessor;
    _services = services;
  }

  public static T GetService<T>()
  {
    T service;
    try
    {
      if (_accessor != null && _accessor.HttpContext != null)
        service = _accessor.HttpContext.RequestServices.GetService<T>();

      else if (_services != null)
      {
        var serviceProvider = _services.BuildServiceProvider();
        service = serviceProvider.GetRequiredService<T>();
      }

      else
        service = default(T);

      return service;
    }
    catch (Exception ex)
    {
      if (_services != null)
      {
        var serviceProvider = _services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        service = scope.ServiceProvider.GetRequiredService<T>();

        return service;
      }

      throw ex;
    }
  }
}