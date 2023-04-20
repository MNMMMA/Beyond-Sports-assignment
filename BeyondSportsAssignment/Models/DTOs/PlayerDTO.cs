namespace BeyondSportsAssignment.Models
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string? PlayerName { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public int CurrentTeamId { get; set; }
        public DateTime LastTransferDate { get; set; }
    }
}
