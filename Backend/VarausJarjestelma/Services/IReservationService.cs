using VarausJarjestelma.Models;

namespace VarausJarjestelma.Services
{
    public interface IReservationService
    {
        public Task<ReservationDTO> CreateReservationAsync(ReservationDTO dto);
        public Task<Boolean> DeleteReservationAsync(long id);
        public Task<ReservationDTO> GetReservationAsync(long id);
        public Task<IEnumerable<ReservationDTO>> GetReservationsAsync();
        public Task<ReservationDTO> UpdateReservationAsync(ReservationDTO reservation);
    }
}
