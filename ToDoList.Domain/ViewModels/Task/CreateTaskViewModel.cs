using ToDoList.Domain.Enum;

namespace ToDoList.Domain.ViewModels.Task;

public class CreateTaskViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ImportanceType ImportanceType { get; set; }

    public void Validate()
    {
        if (string.IsNullOrEmpty(Name))
            throw new ArgumentNullException(Name, "Укажите имя!");
        
        if(string.IsNullOrEmpty(Description))
            throw new ArgumentNullException(Name, "Описание!");
    }
}