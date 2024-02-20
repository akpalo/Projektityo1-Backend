using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem2022.Middleware;
using System.Security.Claims;
using VarausJarjestelma.Models;
using VarausJarjestelma.Services;

namespace VarausJarjestelma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _service;
        private readonly IUserAuthenticationService _authenticationService; 
        
        public ReservationsController(IReservationService Service, IUserAuthenticationService authenticationService)
        {
            _service = Service;
            _authenticationService = authenticationService;
        }

        //GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return Ok(await _service.GetReservationsAsync());
        }

        //GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(long id)
        {
            var reservation = await _service.GetReservationAsync(id);
            {
                if (reservation == null)
                {
                    return NotFound();
                }
                return reservation;
            }
        }

        //PUT: api/Reservations/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutReservation(long id,ReservationDTO reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, reservation);

            if (!isAllowed) //jos ei
            {
                return Unauthorized();
            }

            ReservationDTO updatedReservation = await _service.UpdateReservationAsync(reservation);
            if (updatedReservation == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        //POST: api/Reservations
        [HttpPost]
        
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservation)
        {
            /*bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, reservation);

            if (!isAllowed) //jos ei
            {
                return Unauthorized();
            }*/

            reservation = await _service.CreateReservationAsync(reservation);
            if (reservation == null)
            {
                return Problem();
            }

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        //DELETE: api/Reservations/5
        [HttpDelete("{id}")]
       
        public async Task<IActionResult> DeleteReservation(long id)
        {
            //tarkista oikeus
            /*ReservationDTO reservation = new ReservationDTO();
            reservation.Id = id;
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, reservation);*/

            if (await _service.DeleteReservationAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }


        }


    }
