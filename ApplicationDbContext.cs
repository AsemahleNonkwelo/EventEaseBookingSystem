using Microsoft.EntityFrameworkCore;
using EventEaseBooking.Models;

namespace EventEaseBooking.Data
{
    /// <summary>
    /// Application EF Core DbContext for EventEaseBooking.
    /// Provides DbSet properties and central place for model configuration.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Initialize DbSet properties to non-null to satisfy nullable reference types.
        public DbSet<Venue> Venues { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply any IEntityTypeConfiguration<> implementations in this assembly (future-proof).
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Venue configuration
            modelBuilder.Entity<Venue>(b =>
            {
                b.HasKey(v => v.VenueId);

                b.Property(v => v.VenueName)
                 .IsRequired()
                 .HasMaxLength(200);

                b.Property(v => v.Location)
                 .IsRequired()
                 .HasMaxLength(200);

                b.Property(v => v.ImageUrl)
                 .HasMaxLength(500);

                b.Property(v => v.Capacity)
                 .HasDefaultValue(0);

                // leave WithMany() because Venue currently does not declare a collection navigation
                b.HasMany<Event>()
                 .WithOne(e => e.Venue)
                 .HasForeignKey(e => e.VenueId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // Event configuration
            modelBuilder.Entity<Event>(b =>
            {
                b.HasKey(e => e.EventId);

                b.Property(e => e.EventName)
                 .IsRequired()
                 .HasMaxLength(200);

                b.Property(e => e.Description)
                 .HasMaxLength(2000);

                b.Property(e => e.ImageUrl)
                 .HasMaxLength(500);

                b.Property(e => e.EventDate)
                 .HasColumnType("datetime2");

                b.HasIndex(e => e.EventDate);
            });

            // Booking configuration
            modelBuilder.Entity<Booking>(b =>
            {
                b.HasKey(x => x.BookingId);

                b.Property(x => x.BookingDate)
                 .HasColumnType("datetime2");

                b.HasOne(x => x.Event)
                 .WithMany() // Booking collection on Event not defined today
                 .HasForeignKey(x => x.EventId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(x => x.Venue)
                 .WithMany() // Venue collection not defined today
                 .HasForeignKey(x => x.VenueId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasIndex(x => new { x.EventId, x.VenueId });
                b.HasIndex(x => x.BookingDate);
            });
        }
    }
}