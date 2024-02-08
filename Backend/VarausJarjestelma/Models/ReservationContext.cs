using Microsoft.EntityFrameworkCore;

namespace VarausJarjestelma.Models
{
    public class ReservationContext:DbContext
    {
       

        public ReservationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
