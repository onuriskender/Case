namespace Blog.Common.Helpers;

public class ServiceIoc
{
  public class Container
  {
    public static T Resolve<T>() => HttpHelper.GetService<T>();
  }
}