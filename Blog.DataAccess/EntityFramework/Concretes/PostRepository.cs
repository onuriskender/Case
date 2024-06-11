using Blog.Core.DataAccess.Concretes;
using Blog.DataAccess.Abstracts;
using Blog.DataAccess.EntityFramework.Context;
using Blog.Entity.Concretes;

namespace Blog.DataAccess.EntityFramework.Concretes;

public class PostRepository(BlogDbContext context) : Repository<Post, BlogDbContext>(context), IPostRepository;