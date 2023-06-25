using TaskManagementSystem.Models;

namespace TaskManagementSystem.Dtos
{
    public class TeamDto
    {
        public string Name { get; set; }
        public List<Member> Members { get; set; }
    }
}
