using BeyondSportsAssignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BeyondSportsAssignment.DBContext
{
    public class CreateDatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        public CreateDatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("MyDatabase");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
