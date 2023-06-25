namespace TaskManagementSystem.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TodoTask> TodoTasks { get; set;}
    }
}
