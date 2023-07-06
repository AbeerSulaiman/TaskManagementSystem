using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Dtos;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MemberController : Controller
    {
        private readonly TaskDBContext _dbContext;

        public MemberController(TaskDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<MemberDto>>> GetAllMembers()
        {
            var member = await _dbContext.Members.Include(x => x.TasksList).ToListAsync();
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<MemberDto>> GetMemberById(int Id)
        {
            var member = await _dbContext.Members.Include(x => x.TasksList).FirstOrDefaultAsync(x => x.Id == Id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<MemberDto>> AddMember(CreateMemberDto memberList)
        {
            var member = new Member
            {
                Name = memberList.Name,
                TasksList = memberList.TasksList,
                TeamId = memberList.TeamId
            };

            _dbContext.Members.Add(member);
            await _dbContext.SaveChangesAsync();
            return Ok(member);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTeam(int Id, UpdateMemberDto memberDto)
        {
            var member = await _dbContext.Members.FirstOrDefaultAsync(x => x.Id == Id);
            if (member == null)
            {
                return NotFound();
            }
            member.Name = memberDto.Name;
            member.TeamId = memberDto.TeamId;
            member.TasksList = memberDto.TasksList;
            await _dbContext.SaveChangesAsync();
            return Ok("Member Updated Successfully");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMember([FromRoute] int Id)
        {
            var member = await _dbContext.Members.FindAsync(Id);
            if (member == null)
            { return NotFound(); }

            _dbContext.Members.Remove(member);
            await _dbContext.SaveChangesAsync();
            return Ok("Deleted Successfully");
        }
    }
}
