using TaskManagementSystem.Models;

namespace TaskManagementSystem.Dtos
{
    public class CreateTodoTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        //public string PriorityString { get; set; }
        public int Status { get; set; }
        //public string StatusString { get; set; }
        public int TaskListId { get; set; }
        public int MemberId { get; set; }
    }
}
