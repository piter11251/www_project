using Microsoft.EntityFrameworkCore;
using TicketReservationSystem.Entities;

namespace TicketReservationSystem
{
    public class TicketSystemDbContext : DbContext
    {
        private string _connectionString = "Data Source=DESKTOP-396HNS8;Initial Catalog=TicketSystemDb;Integrated Security=True;Trust Server Certificate=True;";
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Event properties

            modelBuilder.Entity<Event>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Event>()
                .Property(e => e.Description)
                .HasMaxLength(255);

            modelBuilder.Entity<Event>()
                .Property(e => e.TicketPrice)
                .HasPrecision(7,2)
                .IsRequired();

            modelBuilder.Entity<Event>()
                .Property(e => e.TotalSeats)
                .IsRequired();

            modelBuilder.Entity<Event>()
                .Property(e => e.Rows)
                .IsRequired();


            modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)
                .WithOne(c => c.User)
                .HasForeignKey<Customer>(c => c.UserId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
