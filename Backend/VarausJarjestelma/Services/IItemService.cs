using VarausJarjestelma.Models;

namespace VarausJarjestelma.Services
{
    public interface IItemService
    {
        public Task<ItemDTO> CreateItemAsync(ItemDTO dto);
        public Task<ItemDTO> GetItemAsync(long id);
        public Task<IEnumerable<ItemDTO>> GetItemsAsync();
        public Task<ItemDTO> UpdateItemAsync(ItemDTO dto);
        public Task<Boolean> DeleteItemAsync(long id);
    }
}
