using HotelApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HotelApp.EF
{
    public class HotelAppDbContext: DbContext
    {
        public virtual DbSet<LoggedInUser> Users { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<HotelImage> HotelImages { get; set; }
        public virtual DbSet<RoomImage> RoomImages { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }

        public HotelAppDbContext(DbContextOptions<HotelAppDbContext> options)
        : base(options) { }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Hotel>()
                .HasMany(c => c.Rooms)
                .WithOne(x => x.Hotel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hotel>()
                .HasMany(c => c.images)
                .WithOne(x => x.Hotel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Room>()
                .HasMany(c => c.images)
                .WithOne(x => x.Room)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Bookings)
                .WithOne(x => x.Customer)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Payments)
                .WithOne(x => x.Customer)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
