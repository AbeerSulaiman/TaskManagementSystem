using TaskManagementSystem.Models;

namespace TaskManagementSystem.Dtos
{
    public class CreateTodoTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public int TaskListId { get; set; }
        public int MemberId { get; set; }
    }
}
