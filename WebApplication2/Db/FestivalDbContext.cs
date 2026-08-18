using Microsoft.EntityFrameworkCore;

public class FestivalDbContext : DbContext
{
    public FestivalDbContext(DbContextOptions<FestivalDbContext> options) : base(options) { }

    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Performance> Performances => Set<Performance>();
    public DbSet<Stage> Stages => Set<Stage>();
    public DbSet<Attendee> Attendees => Set<Attendee>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Performance <-> Stage (many-to-one)
        modelBuilder.Entity<Performance>()
            .HasOne(p => p.StageNavigation)
            .WithMany(s => s.Performances)
            .HasForeignKey(p => p.StageId)
            .OnDelete(DeleteBehavior.Restrict);

        // Performance <-> Artist (many-to-many, EF Core auto-generates join table)
        modelBuilder.Entity<Performance>()
            .HasMany(p => p.Artists)
            .WithMany(a => a.Performances);

        // Ticket <-> Attendee (one-to-one)
        // Ticket.AttendeeId is the real FK; Attendee.TicketId is redundant and ignored.
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AttendeeNavigation)
            .WithOne(a => a.TicketNavigation)
            .HasForeignKey<Ticket>(t => t.AttendeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Attendee>()
            .Ignore(a => a.TicketId);
    }
}