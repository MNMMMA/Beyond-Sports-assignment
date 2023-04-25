namespace BeyondSportsAssignment.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string? PlayerName { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public int CurrentTeamId { get; set; }
        public DateTime? LastTransferDate { get; set; }
        public int? PreviousTeamId { get; set; }
        public int GoalsCurrentSeason { get; set; }
        public int AssistsCurrentSeason { get; set;  }
        public int GamesPlayedCurrentSeason { get; set; }
    }
}
