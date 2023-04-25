namespace BeyondSportsAssignment.Models
{
    public class PlayerDTO
    {
        public string? PlayerName { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public int CurrentTeamId { get; set; }
        public int GoalsCurrentSeason { get; set; }
        public int AssistsCurrentSeason { get; set; }
        public int GamesPlayedCurrentSeason { get; set; }
    }
}
