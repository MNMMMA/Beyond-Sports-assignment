using BeyondSportsAssignment.DBContext;
using BeyondSportsAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BeyondSportsAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerApiController : ControllerBase
    {

        private readonly CreateDatabaseContext _dbContext;

        public PlayerApiController(CreateDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}", Name = "GetPlayer")]
        public async Task<ActionResult<PlayerDTO>> GetPlayer(int id)
        {
            try
            {
                var player = await _dbContext.Players.FindAsync(id);
                if (player == null)
                {
                    return NotFound();
                }
                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while fetching the specified player");
            }
        }

        [HttpGet("{id}", Name = "GetPlayersFromTeam")]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayersFromTeam(int id)
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

        [HttpPost(Name = "AddTeam")]
        public async Task<IActionResult> AddTeam(Player player)
        {
            if (player == null || string.IsNullOrEmpty(player.PlayerName) || player.Height <= 0 || player.Age <= 0)
            {
                return BadRequest("Invalid input data for player.");
            }

            var team = await _dbContext.Teams.FindAsync(player.CurrentTeamId);
            if (team == null)
            {
                return BadRequest("The specified team ID does not exist.");
            }

            var newPlayer = new Player
            {
                PlayerName = player.PlayerName,
                Height = player.Height,
                Age = player.Age,
                CurrentTeamId = player.CurrentTeamId
            };

            _dbContext.Players.Add(newPlayer);
            await _dbContext.SaveChangesAsync();

            return Ok("Added Player");
        }

        [HttpPut("{id}",Name = "UpdatePlayer")]
        public async Task<IActionResult> PutPlayer(int id, PlayerDTO player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            var toUpdatePlayer = await _dbContext.Players.FindAsync(id);
            if (toUpdatePlayer == null)
            {
                return NotFound();
            }

            toUpdatePlayer.PlayerName = player.PlayerName;
            toUpdatePlayer.Height = player.Height;
            toUpdatePlayer.Age = player.Age;
            if (toUpdatePlayer.CurrentTeamId != player.CurrentTeamId)
            {
                toUpdatePlayer.PreviousTeamId = toUpdatePlayer.CurrentTeamId;
                toUpdatePlayer.LastTransferDate = DateTime.Now;
                toUpdatePlayer.CurrentTeamId = player.CurrentTeamId;
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PlayerExists(id))
            {
                return NotFound();
            }

            return NoContent();

        }

        private bool PlayerExists(int id)
        {
            return _dbContext.Players.Any(e => e.Id == id);
        }
    }
}
