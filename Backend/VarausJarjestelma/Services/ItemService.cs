using VarausJarjestelma.Repositories;
using VarausJarjestelma.Models;

namespace VarausJarjestelma.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IUserRepository _userRepository;

        public ItemService(IItemRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ItemDTO> CreateItemAsync(ItemDTO dto)
        {
            Item newItem = await DTOToItem(dto);
            await _repository.AddItemAsync(newItem);
            return ItemToDTO(newItem);
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            Item oldItem = await _repository.GetItemAsync(id);
            if (oldItem == null)
            {
                return false;
            }
            return await _repository.DeleteItemAsync(oldItem);
        }

        public async Task<ItemDTO> GetItemAsync(long id)
        {
            Item item = await _repository.GetItemAsync(id);
            
            if (item != null)
            {
                //Update access count
                item.accessCount++;
                await _repository.UpdateItemAsync(item);
                return ItemToDTO(item);
            }
            return null;
        }

        public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
        {
            IEnumerable<Item> items = await _repository.GetItemsAsync();
            List<ItemDTO> result = new List<ItemDTO>();
            foreach (Item i in items) 
            {
                result.Add(ItemToDTO(i));
            }
            return result;
        }

        public async Task<ItemDTO> UpdateItemAsync(ItemDTO item)
        {
            Item oldItem = await _repository.GetItemAsync(item.Id);
            if(oldItem == null)
            {
                return null;
            }
            oldItem.Name = item.Name;
            oldItem.Description = item.Description;
            oldItem.Images = item.Images;
            oldItem.accessCount++;
            Item updatedItem =await _repository.UpdateItemAsync(oldItem);
            if (updatedItem == null)
            {
                return null;
            }
            return ItemToDTO(updatedItem);
                
        }

        private async Task<Item> DTOToItem(ItemDTO dto)
        {
            Item newItem = new Item();
            newItem.Name = dto.Name;
            newItem.Description = dto.Description;

            //Hae omistaja kannasta
            User owner = await _userRepository.GetUserAsync(dto.Owner);

            if(owner != null)
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
            if(item.Owner != null)
            {
                dto.Owner = item.Owner.UserName;
            }
            return dto;
        }
    }
}
