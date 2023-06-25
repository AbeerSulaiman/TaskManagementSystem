using TaskManagementSystem.Models;

namespace TaskManagementSystem.Dtos
{
    public class CreateTeamDto
    {
        public string Name { get; set; }
        public List<Member> Members { get; set; }

    }
}
