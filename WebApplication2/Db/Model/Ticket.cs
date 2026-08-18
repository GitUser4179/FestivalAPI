public class Ticket
{
    public int Id { get; set; }
    public int AttendeeId { get; set; }
    public Attendee AttendeeNavigation { get; set; } = null!;
    public int PriceSek { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime ValidThrough { get; set; }
}