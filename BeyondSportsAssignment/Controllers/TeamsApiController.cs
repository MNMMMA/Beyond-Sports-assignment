using BeyondSportsAssignment.DBContext;
using BeyondSportsAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeyondSportsAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsApiController : ControllerBase
    {
        private readonly CreateDatabaseContext _dbContext;

        public TeamsApiController(CreateDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetTeams")]
        public async Task<List<Team>> GetTeams()
        {
            return await _dbContext.Teams.ToListAsync();
        }

        [HttpPost("{name}", Name = "AddTeam")]
        public async Task<IActionResult> AddTeam(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Team name cannot be null or empty.");
            }

            var team = new Team { Name = name };

            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTeams),
                team
                );
        }

    }
}
