using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Entities;

namespace ToDoList.DAL.Repositories;

public class TaskRepository : IBaseRepository<TaskEntity>
{
    private readonly AddDbContext _addDbContext;

    public TaskRepository(AddDbContext addDbContext)
    {
        _addDbContext = addDbContext;
    }

    public async Task Create(TaskEntity entity)
    {
        await _addDbContext.Tasks.AddAsync(entity);
        await _addDbContext.SaveChangesAsync();
    }

    public IQueryable<TaskEntity> GetAll()
    {
        return _addDbContext.Tasks;
    }

    public async  Task Delete(TaskEntity entity)
    {
        _addDbContext.Tasks.Remove(entity);
        await _addDbContext.SaveChangesAsync();
    }

    public async Task<TaskEntity> Update(TaskEntity entity)
    {
        _addDbContext.Tasks.Update(entity);
        await _addDbContext.SaveChangesAsync();

        return entity;
    }
}