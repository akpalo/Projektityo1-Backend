using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem2022.Middleware;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using VarausJarjestelma.Models;
using VarausJarjestelma.Services;



namespace VarausJarjestelma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        
        private readonly IItemService _service;
        private readonly IUserAuthenticationService _authenticationService;

        public ItemsController(IItemService service, IUserAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _service = service;
        }

        /// <summary>
        /// Palauttaa kaikki itemit
        /// </summary>
        /// <remarks>
        /// Esimerkkipyyntö:
        /// 
        ///     GET /items
        //GET: api/Items
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            return Ok(await _service.GetItemsAsync());
        }

        /// <summary>
        /// Palauttaa itemit, jonka nimessä hakusana
        /// </summary>
        /// <remarks>
        /// Esimerkkipyyntö:
        /// 
        ///     GET /items/itemin id

        [HttpGet("{query}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> QueryItems(string query)
        {
            return Ok(await _service.QueryItemsAsync(query));
        }

        /// <summary>
        /// Palauttaa haetun itemin ID:n perusteella
        /// </summary>
        /// <remarks>
        /// Esimerkkipyyntö:
        /// 
        ///     GET /items/itemin id
        ///</remarks>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemDTO>> GetItem(long id)
        {
            var item = await _service.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        /// <summary>
        /// Muokkaa itemiä
        /// </summary>
        /// <remarks>
        /// Esimerkkipyyntö:
        /// 
        ///     PUT /items/id
        ///     {
        ///     "name": "Itemin nimi",
        ///     "description": "Itemin lisätiedot",
        ///     "owner": omistajan ID,
        ///     "images": [
        ///       {
        ///         "url": "kuvaosoite",
        ///         "description": "kuvateksti"
        ///       }
        ///               ]
        ///       }
        ///</remarks>
        //PUT: api/Items/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutItem(long id, ItemDTO item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            //tarkistus, onko käyttäjällä oikeus muokata itemiä
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, item);

            if (!isAllowed) //jos ei
            {
                return Unauthorized();
            }

            ItemDTO updatedItem = await _service.UpdateItemAsync(item);
            if (updatedItem == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Lisää uuden itemin
        /// </summary>
        /// <remarks>
        /// Esimerkkipyyntö:
        /// 
        ///     POST /items
        ///     {
        ///     "name": "Itemin nimi",
        ///     "description": "Itemin lisätiedot",
        ///     "owner": omistajan ID,
        ///     "images": [
        ///       {
        ///         "url": "kuvaosoite",
        ///         "description": "kuvateksti"
        ///       }
        ///               ]
        ///       }
        ///</remarks>
        //POST: api/Items
        [HttpPost]
        

        public async Task<ActionResult<ItemDTO>> PostItem(ItemDTO item)
        {
            //tarkistus, onko käyttäjällä oikeus muokata itemiä
            /*bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, item);

            if (!isAllowed) //jos ei
            {
                return Unauthorized();
            }*/

            ItemDTO newItem = await _service.CreateItemAsync(item);
            if (newItem == null)
            {
                return Problem();
            }

            return CreatedAtAction("GetItem", new { id = newItem.Id }, newItem);
        }

        /// <summary>
        /// Poistaa itemin
        /// </summary>
        /// <remarks>
        /// Esimerkkipyyntö:
        /// 
        ///     DELETE /items/itemin id
        ///</remarks>
        //DELETE: api/Items/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteItem(long id)

        {
            //tarkista oikeus
            ItemDTO item = new ItemDTO();
            item.Id = id;
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, item);

            if (await _service.DeleteItemAsync(id))
            {
                return Ok();
            }
            return NotFound();


        }



    }
}
