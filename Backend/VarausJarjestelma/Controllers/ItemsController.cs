using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using VarausJarjestelma.Models;
using VarausJarjestelma.Services;



namespace VarausJarjestelma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        //poista context!!!
        //private readonly ReservationContext _context;
        private readonly IItemService _service;

        public ItemsController(/*ReservationContext context*/ IItemService service)
        {
            //_context = context;
            _service = service;
        }

        //GET: api/Items
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            return Ok(await _service.GetItemsAsync());
        }

        //GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(long id)
        {
            var item = await _service.GetItemAsync(id);

            if (item == null) 
            {
                return NotFound();
            }
            return item;
        }

        //PUT: api/Items/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutItem(long id, ItemDTO item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            

            ItemDTO updatedItem = await _service.UpdateItemAsync(item);

            if(updatedItem == null)
            {
                return NotFound();
            }
            return NoContent();

            
        }

        //POST: api/Items
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<ItemDTO>> PostItem(ItemDTO item)
        {
            ItemDTO newItem = await _service.CreateItemAsync(item);
            if (newItem == null) 
            {
                return Problem();
            }

            return CreatedAtAction("GetItem", new { id = newItem.Id }, newItem);
        }

        //DELETE: api/Items/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteItem(long id)
        {
            if (await _service.DeleteItemAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }

        /*private bool ItemExists(long id)
        {
            return _context.Items.Any(e => e.Id == id);
        }

        private Item DTOToItem(ItemDTO dto)
        {
            Item newItem = new Item();
            newItem = dto.Name;
            newItem.Description = dto.Description;

            User owner = _context.Users.Where(x => x.UserName == dto.Owner).FirstOrDefault();

            if (owner != null)
            {
                newItem.Owner = owner;
            }
            newItem.Images = dto.Images;
            newItem.accessCount = 0;
            return newItem;
        }
        private ItemDTO ItemToDTO(Item item)
        {
            ItemDTO dto = new ItemDTO();
            dto.Id = item.Id;
            dto.Name = item.Name;
            dto.Description = item.Description;
            dto.Images = item.Images;
            if (item.Owner != null)
            {
                dto.Owner = item.Owner.UserName;
            }
            return dto;
        }*/
         
    }
}
