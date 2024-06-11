using System.Linq.Expressions;
using Blog.Core.Entities.Interfaces;

namespace Blog.Core.DataAccess.Abstracts;

public interface IRepository<T> where T : IBaseEntity
{
  /// <summary>
  /// TODO: summary
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  Task<int> AddAsync(T entity);
  
  /// <summary>
  /// TODO: summary
  /// </summary>
  /// <param name="expression"></param>
  /// <returns></returns>
  Task<List<T>> ToListAsync(Expression<Func<T, bool>>? expression = null);
  
  /// <summary>
  /// TODO: summary
  /// </summary>
  /// <param name="expression"></param>
  /// <returns></returns>
  Task<bool> AnyAsync(Expression<Func<T, bool>>? expression = null);
  
  /// <summary>
  /// TODO: summary
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  Task<int> UpdateAsync(T entity);
  
  /// <summary>
  /// TODO: summary
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  Task<int> DeleteAsync(T entity);
  
  /// <summary>
  /// TODO: summary
  /// </summary>
  /// <param name="expression"></param>
  /// <returns></returns>
  Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

  /// <summary>
  /// TODO: sum
  /// </summary>
  /// <param name="expression"></param>
  /// <returns></returns>
  Task<int> CountAsync(Expression<Func<T, bool>>? expression = null);
  
  /// <summary>
  /// TODO:summary
  /// </summary>
  /// <returns></returns>
  Task<int> SaveAsync();
}