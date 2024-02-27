using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoList.DAL.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enum;
using ToDoList.Domain.Extensions;
using ToDoList.Domain.Response;
using ToDoList.Domain.ViewModels.Task;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service.Implementations;

public class TaskService : ITaskService
{
    private readonly IBaseRepository<TaskEntity> _taskRepository;
    private ILogger<TaskService> _logger;

    public TaskService(IBaseRepository<TaskEntity> taskRepository, ILogger<TaskService> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    public async Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel createTaskViewModel)
    {
        try
        {
            createTaskViewModel.Validate();
            _logger.LogInformation($"Запрос на создание задачи - {createTaskViewModel.Name}");

            TaskEntity? task = await _taskRepository.GetAll()
                .Where(x => x.CreatedTime.Date == DateTime.Today)
                .FirstOrDefaultAsync(x => x.Name == createTaskViewModel.Name);

            if (task != null)
            {
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Задача с таким названием уже есть",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            task = new TaskEntity()
            {
                Name = createTaskViewModel.Name,
                Description = createTaskViewModel.Description,
                ImportanceType = createTaskViewModel.ImportanceType,
                CreatedTime = DateTime.Now,
                IsDone = false
            };

            await _taskRepository.Create(task);

            _logger.LogInformation($"Задача создалась: {task.Name} - {task.CreatedTime}");
            return new BaseResponse<TaskEntity>()
            {
                Description = "Задача создалась",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[TaskService.Create]: {exception.Message}");

            return new BaseResponse<TaskEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<TaskViewModel>>> GetTasks()
    {
        try
        {
            List<TaskViewModel> tasks = await _taskRepository.GetAll()
                .Select(x => new TaskViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IsDone = x.IsDone == true ? "Готова" : "Не готова",
                    ImportanceType = x.ImportanceType.GetDisplayName(),
                    Created = x.CreatedTime.ToLongDateString()
                })
                .ToListAsync();

            return new BaseResponse<IEnumerable<TaskViewModel>>
            {
                Data = tasks,
                StatusCode = StatusCode.OK,
            };
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"[TaskService.Create]: {exception.Message}");

            return new BaseResponse<IEnumerable<TaskViewModel>>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}