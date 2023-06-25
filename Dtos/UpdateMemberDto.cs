using TaskManagementSystem.Models;

namespace TaskManagementSystem.Dtos
{
    public class UpdateMemberDto
    {
        public string Name { get; set; }
        public int TeamId { get; set; }
        public List<TaskList> TasksList { get; set; }
    }
}
