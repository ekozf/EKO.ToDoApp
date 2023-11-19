using EKO.ToDoApp.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EKO.ToDoApp.Infrastructure.Storage;

public sealed class ToDoDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Maps to all users
    /// </summary>
    public DbSet<UserEntity> Users { get; set; }

    /// <summary>
    /// Maps to all ToDos
    /// </summary>
    public DbSet<ToDoTaskEntity> ToDos { get; set; }

    /// <summary>
    /// Maps to all created ToDo Lists
    /// </summary>
    public DbSet<TaskList> ToDoLists { get; set; }

    public ToDoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

#if DEBUG
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("ToDoDev"));
#else
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("ToDoProd"));
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.FirstName)
                .IsRequired();

            entity.Property(x => x.LastName)
                .IsRequired();

            entity.Property(x => x.Email)
                .IsRequired();

            entity.Property(x => x.Password)
                .IsRequired();

            entity.Property(x => x.Salt)
                .IsRequired();
        });

        modelBuilder.Entity<ToDoTaskEntity>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                .IsRequired();

            entity.Property(x => x.IsCompleted)
                .IsRequired();

            entity.Property(x => x.Urgency)
                .IsRequired();
        });

        modelBuilder.Entity<TaskList>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                .IsRequired();

            entity.Property(x => x.UserId)
                .IsRequired();
        });
    }
}
