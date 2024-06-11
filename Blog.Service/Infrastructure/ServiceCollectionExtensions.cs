using Blog.DataAccess.Abstracts;
using Blog.DataAccess.EntityFramework.Concretes;
using Blog.Entity.Concretes;
using Blog.Service.Abstracts;
using Blog.Service.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Service.Infrastructure;

public static class ServiceCollectionExtensions
{
  public static void AddRepositories(this IServiceCollection services)
  {
    services.AddScoped<IAppUserRepository, AppUserRepository>();
    services.AddScoped<IPostRepository, PostRepository>();
    services.AddScoped<IPostViewerRepository, PostViewerRepository>();
  }

  public static void AddServices(this IServiceCollection services)
  {
    services.AddScoped<UserManager<AppUser>>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IPostService, PostService>();
    services.AddScoped<IPostViewerService, PostViewerService>();
  }
}