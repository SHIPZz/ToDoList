using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL;

public class AddDbContext : DbContext
{
    public AddDbContext(DbContextOptions<AddDbContext> options ) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<TaskEntity> Tasks { get; set; }
}