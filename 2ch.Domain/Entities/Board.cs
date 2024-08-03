namespace _2ch.Domain.Entities
{
    public class Board
    {
        public Guid BoardId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
