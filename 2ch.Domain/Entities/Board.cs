namespace _2ch.Domain.Entities
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PostsPerHour { get; set; }
        public int UniqueIPs { get; set; }
        public long TotalPosts { get; set; }
        //public ICollection<Thread> Threads { get; set; } = new List<Thread>();
    }
}
