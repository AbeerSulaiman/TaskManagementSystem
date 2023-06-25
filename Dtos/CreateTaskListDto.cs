using TaskManagementSystem.Models;

namespace TaskManagementSystem.Dtos
{
    public class CreateTaskListDto
    {
        public string Name { get; set; }
        public List<TodoTask> TodoTasks { get; set; }
    }
}
