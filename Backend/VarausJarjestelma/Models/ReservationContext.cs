using Microsoft.EntityFrameworkCore;

namespace VarausJarjestelma.Models
{
    public class ReservationContext:DbContext
    {
        internal readonly object Reservation;

        public ReservationContext(DbContextOptions<ReservationContext> options)
           : base(options)
        {
        }

        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
