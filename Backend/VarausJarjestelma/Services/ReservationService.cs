using VarausJarjestelma.Repositories;
using VarausJarjestelma.Models;

namespace VarausJarjestelma.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationrepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;


        public ReservationService(IReservationrepository repository, IUserRepository userRepository, IItemRepository itemRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _itemRepository = itemRepository;
        }
        public async Task<ReservationDTO> CreateReservationAsync(ReservationDTO dto)
        {
            if(dto.StartTime>= dto.EndTime)
            {
                return null;
            }
            Item target = await _itemRepository.GetItemAsync(dto.Target);
            if(target == null)
            {
                return null;
            }
            IEnumerable<Reservation> reservations = await _repository.GetReservationsAsync(target, dto.StartTime, dto.EndTime);
            if(reservations.Count() > 0)
            {
                return null;
            }
            Reservation newReservation = await DTOToReservationAsync(dto);

            newReservation = await _repository.AddReservationAsync(newReservation);

            return ReservationToDTO(newReservation);
        }

        public Task<bool> DeleteReservationAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ReservationDTO> GetReservationAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReservationDTO>> GetReservationAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReservationDTO> UpdateReservationAsync(ReservationDTO reservation)
        {
            throw new NotImplementedException();
        }

        private async Task<Reservation> DTOToReservationAsync(ReservationDTO dto)
        {
            Reservation res = new Reservation();
            User owner = await _userRepository.GetUserAsync(dto.Owner); 
            if(owner == null)
            {
                return null;
            }
            Item target = await _itemRepository.GetItemAsync(dto.Target);
            if(target == null)
            {
                return null;
            }
            res.Id = dto.Id;
            res.Owner = owner;
            res.Target = target;
            res.StartTime = dto.StartTime;
            res.EndTime = dto.EndTime;

            return res;
        }

        private ReservationDTO ReservationToDTO(Reservation res)
        {
            ReservationDTO dto = new ReservationDTO();

            dto.Id = res.Id;
            dto.Target = res.Target.Id;
            dto.Owner = res.Owner.UserName;
            dto.StartTime = res.StartTime;
            dto.EndTime = res.EndTime;

            return dto;
        }
    }
}
