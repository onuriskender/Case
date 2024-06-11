using Blog.Common.Helpers;
using Blog.DataAccess.Abstracts;

namespace Blog.Service;

internal class Repositories
{
  public static IAppUserRepository AppUserRepository => ServiceIoc.Container.Resolve<IAppUserRepository>();
  public static IPostRepository PostRepository => ServiceIoc.Container.Resolve<IPostRepository>();
  public static IPostViewerRepository PostViewerRepository => ServiceIoc.Container.Resolve<IPostViewerRepository>();
}