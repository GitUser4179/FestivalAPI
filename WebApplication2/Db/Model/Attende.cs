public class Attendee
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? TicketId { get; set; }
    public Ticket? TicketNavigation { get; set; }
}