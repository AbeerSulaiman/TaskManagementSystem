using TaskManagementSystem.Models;

namespace TaskManagementSystem.Dtos
{
    public class UpdateTaskListDto
    {
        public string Name { get; set; }
        public List<TodoTask> TodoTasks { get; set; }
    }
}
