namespace TaskManagementSystem.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set;}
        public int Priority { get; set;}
        public int TaskListId { get; set;}
        public int MemberId { get; set; }

    }

    

}
