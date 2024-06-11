using System.Linq.Expressions;
using Blog.Core.DataAccess.Abstracts;
using Blog.Core.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.DataAccess.Concretes;

public abstract class Repository<TEntity, TContext>(TContext context) : IRepository<TEntity>
  where TEntity : class, IBaseEntity
  where TContext : DbContext
{
  private readonly DbSet<TEntity> _set = context.Set<TEntity>();

  public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
  {
    return await _set.FirstOrDefaultAsync(expression);
  }

  public async Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>>? expression = null)
  {
    if (expression is null)
      return await _set.ToListAsync();

    return await _set.Where(expression).ToListAsync();
  }

  public async Task<int> AddAsync(TEntity entity)
  {
    await _set.AddAsync(entity);
    return await SaveAsync();
  }

  public async Task<int> DeleteAsync(TEntity entity)
  {
    _set.Remove(entity);
    return await SaveAsync();
  }

  public async Task<int> UpdateAsync(TEntity entity)
  {
    context.Entry<TEntity>(entity).State = EntityState.Modified;
    return await SaveAsync();
  }

  public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
  {
    bool exist;

    if (expression is null)
      exist = await _set.AnyAsync();
    else
      exist = await _set.AnyAsync(expression);

    return exist;
  }

  public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null)
  {
    if (expression is null)
      return await _set.CountAsync();

    return await _set.CountAsync(expression);
  }

  public async Task<int> SaveAsync() => await context.SaveChangesAsync();
}