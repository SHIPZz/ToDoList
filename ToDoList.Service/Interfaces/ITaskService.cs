﻿using ToDoList.Domain.Entities;
using ToDoList.Domain.Filters.Task;
using ToDoList.Domain.Response;
using ToDoList.Domain.ViewModels.Task;

namespace ToDoList.Service.Interfaces;

public interface ITaskService
{
    Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel createTaskViewModel);
    Task<IBaseResponse<IEnumerable<TaskViewModel>>> GetTasks(TaskFilter taskFilter);
}