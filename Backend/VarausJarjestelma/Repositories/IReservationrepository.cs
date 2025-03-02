﻿using VarausJarjestelma.Models;

namespace VarausJarjestelma.Repositories
{
    public interface IReservationrepository
    {
        public Task<Reservation> GetReservationAsync(long id);
        public Task<IEnumerable<Reservation>> GetReservationsAsync();
        public Task<IEnumerable<Reservation>> GetReservationAsync(Item target, DateTime start, DateTime end);
        public Task<Reservation> AddReservationAsync(Reservation reservation);
        public Task<Reservation> UpdateReservationAsync(Reservation reservation);
        public Task<Boolean> DeleteReservationAsync(Reservation reservation);



    }
}
