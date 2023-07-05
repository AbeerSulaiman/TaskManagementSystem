namespace TaskManagementSystem.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set;}
        public Priority Priority { get; set;}
        public Status Status { get; set; }
        public int TaskListId { get; set;}
        public int MemberId { get; set; }

    }

    

}
