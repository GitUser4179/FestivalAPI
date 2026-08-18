public class Stage
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Performance> Performances { get; set; } = new();
}