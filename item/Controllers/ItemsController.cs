

using item.Data.Models;
using item.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace item.Controllers
{
    [Route("api/receiver/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet("list")]
        public Task<IEnumerable<Item>> ItemListAsync()
        {
            var itemList = _itemRepository.GetItemListAsync();
            return itemList;

        }
        [HttpGet("filterlist")]
        public Task<Item> GetitemByIdAsync(int Id)
        {
            return _itemRepository.GetItemByIdAsync(Id);
        }

        [HttpPost("additem")]
        public Task<Item> AdditemAsync(Item item)
        {
            var itemData = _itemRepository.AddItemAsync(item);
            return itemData;
        }

        [HttpPost("sendoffer")]
        public bool SenditemOfferAsync(ItemDTO itemDTO)
        {
            bool isSent = false;
            if (itemDTO != null)
            {
                isSent = _itemRepository.SendItemOffer(itemDTO);

                return isSent;
            }
            return isSent;
        }

    }
}