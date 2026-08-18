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
        // Performance -> Stage
        modelBuilder.Entity<Performance>()
            .HasOne(p => p.StageNavigation)
            .WithMany(s => s.Performances)
            .HasForeignKey(p => p.StageId)
            .OnDelete(DeleteBehavior.Restrict);

        // Performance <-> Artist
        modelBuilder.Entity<Performance>()
            .HasMany(p => p.Artists)
            .WithMany(a => a.Performances);

        // Ticket -> Attendee
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AttendeeNavigation)
            .WithOne(a => a.TicketNavigation)
            .HasForeignKey<Ticket>(t => t.AttendeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Attendee>()
            .Ignore(a => a.TicketId);


        // --------------------
        // ARTISTS
        // --------------------

        modelBuilder.Entity<Artist>().HasData(
            new Artist
            {
                Id = 1,
                Name = "The Weekenders"
            },
            new Artist
            {
                Id = 2,
                Name = "Northern Lights"
            },
            new Artist
            {
                Id = 3,
                Name = "Stockholm Beats"
            }
        );


        // --------------------
        // STAGES
        // --------------------

        modelBuilder.Entity<Stage>().HasData(
            new Stage
            {
                Id = 1,
                Name = "Main Stage"
            },
            new Stage
            {
                Id = 2,
                Name = "Rock Stage"
            },
            new Stage
            {
                Id = 3,
                Name = "Electronic Stage"
            }
        );


        // --------------------
        // PERFORMANCES
        // --------------------

        modelBuilder.Entity<Performance>().HasData(
            new Performance
            {
                Id = 1,
                Genre = "Pop",
                LengthMinutes = 60,
                StageId = 1,
                PerformanceTime = new DateTime(2026, 8, 22, 18, 0, 0)
            },
            new Performance
            {
                Id = 2,
                Genre = "Rock",
                LengthMinutes = 75,
                StageId = 2,
                PerformanceTime = new DateTime(2026, 8, 22, 20, 0, 0)
            },
            new Performance
            {
                Id = 3,
                Genre = "Electronic",
                LengthMinutes = 90,
                StageId = 3,
                PerformanceTime = new DateTime(2026, 8, 22, 22, 0, 0)
            }
        );


        // --------------------
        // PERFORMANCE <-> ARTIST
        // Generated join table
        // --------------------

        modelBuilder.Entity<Performance>()
            .HasMany(p => p.Artists)
            .WithMany(a => a.Performances)
            .UsingEntity<Dictionary<string, object>>(
                "ArtistPerformance",
                j => j
                    .HasOne<Artist>()
                    .WithMany()
                    .HasForeignKey("ArtistsId")
                    .HasPrincipalKey(a => a.Id),
                j => j
                    .HasOne<Performance>()
                    .WithMany()
                    .HasForeignKey("PerformancesId")
                    .HasPrincipalKey(p => p.Id),
                j =>
                {
                    j.HasKey("ArtistsId", "PerformancesId");

                    j.HasData(
                        new
                        {
                            ArtistsId = 1,
                            PerformancesId = 1
                        },
                        new
                        {
                            ArtistsId = 2,
                            PerformancesId = 2
                        },
                        new
                        {
                            ArtistsId = 3,
                            PerformancesId = 3
                        }
                    );
                });


        // --------------------
        // ATTENDEES
        // --------------------

        modelBuilder.Entity<Attendee>().HasData(
            new
            {
                Id = 1,
                Name = "Anna Andersson"
            },
            new
            {
                Id = 2,
                Name = "Erik Johansson"
            },
            new
            {
                Id = 3,
                Name = "Sara Karlsson"
            }
        );


        // --------------------
        // TICKETS
        // --------------------

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                Id = 1,
                AttendeeId = 1,
                PriceSek = 750,
                PurchaseDate = new DateTime(2026, 7, 1),
                ValidThrough = new DateTime(2026, 8, 24)
            },
            new Ticket
            {
                Id = 2,
                AttendeeId = 2,
                PriceSek = 750,
                PurchaseDate = new DateTime(2026, 7, 5),
                ValidThrough = new DateTime(2026, 8, 24)
            },
            new Ticket
            {
                Id = 3,
                AttendeeId = 3,
                PriceSek = 1000,
                PurchaseDate = new DateTime(2026, 7, 10),
                ValidThrough = new DateTime(2026, 8, 24)
            }
        );
    }
}