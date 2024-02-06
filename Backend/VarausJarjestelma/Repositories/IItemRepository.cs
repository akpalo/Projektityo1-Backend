using VarausJarjestelma.Models;

namespace VarausJarjestelma.Repositories
{
    public interface IItemRepository
    {
        public Task<Item> GetItemAsync(long id);
        public Task<IEnumerable<Item>> GetItemsAsync();
        public Task<Item> AddItemAsync(Item item);
        public Task<Item> UpdateItemAsync(Item item);
        public Task<bool> DeleteItemAsync(Item item);
    }
}
