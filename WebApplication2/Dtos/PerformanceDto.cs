namespace WebApplication2.Dtos
{
    public class PerformanceDto
    {
        public int? Id { get; set; }
        public string Genre { get; set; } = null!;
        public int LengthMinutes { get; set; }
        public List<Artist> Artists { get; set; } = null!;
        public int StageId { get; set; }
        public Stage StageNavigation { get; set; } = null!;
        public DateTime PerformanceTime { get; set; }
    }
}
