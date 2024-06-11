using Blog.Common.Helpers;
using Blog.Entity.Concretes;
using Blog.Service.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Blog.Service;

public static class Services
{
  public static UserManager<AppUser> UserManager => ServiceIoc.Container.Resolve<UserManager<AppUser>>();
  public static IUserService UserService => ServiceIoc.Container.Resolve<IUserService>();
  public static IPostService PostService => ServiceIoc.Container.Resolve<IPostService>();
  public static IPostViewerService PostViewerService => ServiceIoc.Container.Resolve<IPostViewerService>();
}