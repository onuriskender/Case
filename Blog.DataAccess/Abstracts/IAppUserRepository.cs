using Blog.Core.DataAccess.Abstracts;
using Blog.Entity.Concretes;

namespace Blog.DataAccess.Abstracts;

public interface IAppUserRepository : IRepository<AppUser>
{
}