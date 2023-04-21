using Azure;
using BeyondSportsAssignment.DBContext;
using BeyondSportsAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace BeyondSportsAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerApiController : ControllerBase
    {

        private readonly CreateDatabaseContext _dbContext;

        public PlayerApiController(CreateDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetPlayer/{id}")]
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



        [HttpPost("AddPlayerToTeam")]
        public async Task<IActionResult> AddPlayerToTeam(PlayerDTO player)
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

        [HttpPut("UpdatePlayer/{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            var playerToUpdate = await _dbContext.Players.FindAsync(id);
            if (playerToUpdate == null || !TeamExists(player.CurrentTeamId))
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(player.PlayerName))
            {
                playerToUpdate.PlayerName = player.PlayerName;
            }
            if (player.Height >= 0)
            {
                playerToUpdate.Height = player.Height;
            }
            if (player.Age >= 0)
            {
                playerToUpdate.Age = player.Age;
            }

            if (playerToUpdate.CurrentTeamId != player.CurrentTeamId)
            {
                playerToUpdate.PreviousTeamId = playerToUpdate.CurrentTeamId;
                playerToUpdate.LastTransferDate = DateTime.Now;
                playerToUpdate.CurrentTeamId = player.CurrentTeamId;
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

        [HttpDelete("DeletePlayer/{id}")]
        [SwaggerOperation(OperationId = "DeletePlayer")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _dbContext.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _dbContext.Players.Remove(player);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        private bool PlayerExists(int id)
        {
            return _dbContext.Players.Any(e => e.Id == id);
        }
        private bool TeamExists(int id)
        {
            return _dbContext.Teams.Any(e => e.Id == id);
        }
    }
}
