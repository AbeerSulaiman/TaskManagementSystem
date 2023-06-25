using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Dtos;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamController : Controller
    {
        private readonly TaskDBContext _dbContext;

        public TeamController(TaskDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            return await _dbContext.Teams.Include(x => x.Members).ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Team>> GetTeamById(int Id)
        {
            var team = await _dbContext.Teams.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == Id);
            if(team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<TeamDto>> AddTeam(CreateTeamDto teamList)
        {
            var team = new Team
            {
                Name = teamList.Name,
                Members = teamList.Members
            };

            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();

            return Ok(team);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTeam(int Id, UpdateTeamDto teamlist)
        {
            var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Id == Id);
            if (team == null)
            {
                return NotFound();
            }
            team.Name = teamlist.Name;
            team.Members = teamlist.Members;
            await _dbContext.SaveChangesAsync();
            return Ok("Team Updated Successfully");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTeam([FromRoute] int Id)
        {
            var team = await _dbContext.Teams.FindAsync(Id);
            if (team == null)
            { return NotFound(); }

            _dbContext.Teams.Remove(team);
            await _dbContext.SaveChangesAsync();
            return Ok("Deleted Successfully");
        }
    }
}
