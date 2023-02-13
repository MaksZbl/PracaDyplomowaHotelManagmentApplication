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
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }

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

            //Hotel
            modelBuilder.Entity<Room>()
                 .HasOne(h => h.Hotel)
                 .WithMany(r => r.Rooms)
                 .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Rate>()
                 .HasOne(h => h.Hotel)
                 .WithMany(r => r.Rates)
                 .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LoggedInUser>()
                 .HasOne(h => h.Hotel)
                 .WithMany(l => l.Employees)
                 .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<HotelImage>()
                 .HasOne(h => h.Hotel)
                 .WithMany(i => i.images)
                 .OnDelete(DeleteBehavior.SetNull);

            //Room
            modelBuilder.Entity<Booking>()
                .HasOne(r => r.Room)
                .WithOne(b => b.Booking)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RoomImage>()
                .HasOne(r => r.Room)
                .WithMany(r => r.images)
                .OnDelete(DeleteBehavior.SetNull);

            //LoggedInUser
            modelBuilder.Entity<Booking>()
                .HasOne(l => l.LoggedInUser)
                .WithMany(b => b.Bookings)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Rate>()
                .HasOne(l => l.LoggedInUser)
                .WithMany(p => p.Rates)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
