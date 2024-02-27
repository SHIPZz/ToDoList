using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Enum;

public enum ImportanceType
{
    [Display(Name = "Легкая")]
    Easy = 1,
    [Display(Name = "Средняя")]
    Middle = 2,
    [Display(Name = "Важная")]
    Important = 3,
}