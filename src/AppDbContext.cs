using Microsoft.EntityFrameworkCore;
using Booking.Models;

namespace Booking
{
    public class AppDbContext : DbContext
    {

        public DbSet<Show> Shows { get; set; }
        public DbSet<Salon> Salons { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Seat> Seats { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Show>().ToTable("show");
            builder.Entity<Show>().HasKey(p => p.Id);
            builder.Entity<Show>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Show>().Property(p => p.Title).IsRequired().HasMaxLength(30);
            builder.Entity<Show>().Property(p => p.StartTime).IsRequired();
            builder.Entity<Show>().Property(p => p.EndTime).IsRequired();
            builder.Entity<Show>().Property(p => p.Summary).IsRequired();
            builder.Entity<Show>().Property(p => p.Price).IsRequired();
            builder.Entity<Show>().Property(p => p.SalonId).IsRequired();
           
           
            builder.Entity<Seat>().ToTable("seat");
            builder.Entity<Seat>().HasKey(p => p.Id);
            builder.Entity<Seat>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Seat>().Property(p => p.X).IsRequired();
            builder.Entity<Seat>().Property(p => p.Y).IsRequired();
            builder.Entity<Seat>().Property(p => p.SalonId).IsRequired();


            builder.Entity<Salon>().ToTable("salon");
            builder.Entity<Salon>().HasKey(p => p.Id);
            builder.Entity<Salon>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Salon>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Salon>().Property(p => p.SeatWidth ).IsRequired();
            builder.Entity<Salon>().Property(p => p.SeatHeight).IsRequired();
            // builder.Entity<Salon>().HasMany(p => p.Seats).WithOne(p => p.Salon).HasForeignKey(p => p.SalonId);
            // builder.Entity<Salon>().HasMany(p => p.Shows).WithOne(p => p.Salon).HasForeignKey(p => p.SalonId);


            builder.Entity<Ticket>().ToTable("ticket");
            builder.Entity<Ticket>().HasKey(p => p.Id);
            builder.Entity<Ticket>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Ticket>().Property(p => p.UserId).IsRequired();
            // builder.Entity<Ticket>().HasOne(p => p.Seat).WithMany(p => p.Tickets).HasForeignKey(p => p.SeatId);
        }

    }
}
