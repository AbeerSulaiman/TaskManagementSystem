namespace TaskManagementSystem.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public List<TaskList> TasksList { get; set; }
      

    }
}
