using BeyondSportsAssignment.DBContext;
using BeyondSportsAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeyondSportsAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsApiController : ControllerBase
    {
        private readonly CreateDatabaseContext _dbContext;

        public TeamsApiController(CreateDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetTeams")]
        public async Task<List<Team>> GetTeams()
        {
            return await _dbContext.Teams.ToListAsync();
        }

        [HttpPost("AddTeam")]
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

        [HttpGet("GetPlayersFromTeam/{id}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayersFromTeam(int id)
        {
            try
            {
                var players = await _dbContext.Players.Where(x => x.CurrentTeamId == id).ToListAsync();
                if (players == null || players.Count == 0)
                {
                    return NotFound("No players found for the specified team.");
                }
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while fetching players for the specified team.");
            }
        }

    }
}
