using System.Linq.Expressions;
using Blog.Core.Entities.Interfaces;

namespace Blog.Core.DataAccess.Abstracts;

public interface IRepository<T> where T : IBaseEntity
{
  /// <summary>
  /// Asynchronously adds the given entity to the database.
  /// </summary>
  /// <param name="entity">The entity to be added to the database.</param>
  /// <returns>The number of state entries written to the database.</returns>
  Task<int> AddAsync(T entity);
  
  /// <summary>
/// Asynchronously retrieves a list of entities from the database that satisfy the given expression. 
/// If no expression is provided, it retrieves all entities.
/// </summary>
/// <param name="expression">A function to test each element for a condition.</param>
/// <returns>A list of entities satisfying the specified condition.</returns>
Task<List<T>> ToListAsync(Expression<Func<T, bool>>? expression = null);

/// <summary>
/// Asynchronously determines whether any element of a sequence satisfies a condition.
/// </summary>
/// <param name="expression">A function to test each element for a condition.</param>
/// <returns>True if any elements in the sequence pass the test in the specified predicate; otherwise, false.</returns>
Task<bool> AnyAsync(Expression<Func<T, bool>>? expression = null);

/// <summary>
/// Asynchronously updates the given entity in the database.
/// </summary>
/// <param name="entity">The entity to be updated.</param>
/// <returns>The number of state entries written to the database.</returns>
Task<int> UpdateAsync(T entity);

/// <summary>
/// Asynchronously removes the given entity from the database.
/// </summary>
/// <param name="entity">The entity to be removed.</param>
/// <returns>The number of state entries written to the database.</returns>
Task<int> DeleteAsync(T entity);

/// <summary>
/// Asynchronously retrieves the first entity satisfying the specified condition or a default value if no such entity is found.
/// </summary>
/// <param name="expression">A function to test each element for a condition.</param>
/// <returns>The first entity satisfying the specified condition, or a default value if no such entity is found.</returns>
Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

/// <summary>
/// Asynchronously counts the number of entities in the database that satisfy the given expression. 
/// If no expression is provided, it counts all entities.
/// </summary>
/// <param name="expression">A function to test each element for a condition.</param>
/// <returns>The number of entities satisfying the specified condition.</returns>
Task<int> CountAsync(Expression<Func<T, bool>>? expression = null);

/// <summary>
/// Asynchronously saves all changes made in this context to the database.
/// </summary>
/// <returns>The number of state entries written to the database.</returns>
Task<int> SaveAsync();
}