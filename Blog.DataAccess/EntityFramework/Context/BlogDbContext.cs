using Blog.Core.Entities.Abstracts;
using Blog.Entity.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blog.DataAccess.EntityFramework.Context;

public class BlogDbContext(DbContextOptions<BlogDbContext> options)
  : IdentityDbContext<AppUser, IdentityRole<int>, int>(options)
{
  public DbSet<AppUser> AppUsers { get; set; }
  public DbSet<Post> Posts { get; set; }
  public DbSet<PostViewer> PostViewers { get; set; }
  
  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    SetBaseProperties();
    return base.SaveChangesAsync(cancellationToken);
  }
  
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<AppUser>(entity =>
    {
      entity.ToTable(name:"AppUsers");
    });
  }

  private void SetBaseProperties()
  {
    var entries = ChangeTracker.Entries<BaseEntity>();
    foreach (var entry in entries)
    {
      SetIfAdded(entry);
      SetIfModified(entry);
      SetIfDeleted(entry);
    }
  }
  
  private void SetIfAdded(EntityEntry<BaseEntity> entityEntry)
  {
    if (entityEntry.State == EntityState.Added)
    {
      entityEntry.Entity.CreatedDate = DateTime.UtcNow;
      entityEntry.Entity.UpdatedDate = DateTime.UtcNow;
      entityEntry.Entity.IsActive = true;
      entityEntry.Entity.IsDeleted = false;
    }
  }

  private void SetIfModified(EntityEntry<BaseEntity> entityEntry)
  {
    if (entityEntry.State == EntityState.Modified)
    {
      entityEntry.Entity.UpdatedDate = DateTime.UtcNow;
    }
  }

  private void SetIfDeleted(EntityEntry<BaseEntity> entityEntry)
  {
    if (entityEntry.State == EntityState.Deleted)
    {
      entityEntry.State = EntityState.Modified;
      entityEntry.Entity.UpdatedDate = DateTime.UtcNow;
      entityEntry.Entity.IsActive = false;
      entityEntry.Entity.IsDeleted = true;
    }
  }
}