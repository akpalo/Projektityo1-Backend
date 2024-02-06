using Microsoft.EntityFrameworkCore;
using VarausJarjestelma.Models;

namespace VarausJarjestelma.Repositories
{
    public class ReservationRepository : IReservationrepository
    {
        private readonly ReservationContext _context;
        public ReservationRepository(ReservationContext context)
        {
            _context = context;
        }

        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return reservation;
        }

        public Task<bool> DeleteReservationAsync(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Task<Reservation> GetReservationAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync(Item target, DateTime start, DateTime end)
        {
            return await _context.Reservations.Include(i => i.Owner).Include(x => x.Target).Where(x => x.Target == target && ((x.StartTime >= start && x.StartTime < end) || (x.EndTime >= start && x.EndTime < end) || (x.StartTime <= start && x.EndTime >= end))).ToListAsync();
        }

        public Task<Reservation> UpdateReservationAsync(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
