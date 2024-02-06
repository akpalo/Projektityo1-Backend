using Microsoft.EntityFrameworkCore;
using VarausJarjestelma.Models;

namespace VarausJarjestelma.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ReservationContext _context;

        public ItemRepository(ReservationContext context)
        {
            _context = context;
        }
        public async Task<Item> AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return null;
            }
            return item;
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            try
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<Item> GetItemAsync(long id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            return item;
        }

       
    }
}
