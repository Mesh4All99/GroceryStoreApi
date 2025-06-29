using GroceryStore.DTO;
using GroceryStore.Models;

namespace GroceryStore.Mapper
{
    public static class DTOMapper
    {
        public static ShowItemsDTO ShowDTOItems(this Item model)
        {
            return new ShowItemsDTO
            {
                ItemId = model.ItemId,
                Name = model.Name,
                ComanyName = model.ComanyName,
                Price = model.Price,
            };
        }
        public static Item ToItemPostDTO(this PostItemDTO model)
        {
            return new Item
            {
                Name = model.Name,
                ComanyName = model.ComanyName,
                Price = model.Price,
                SupplierName = model.SupplierName,
                DateOfPurchase = model.DateOfPurchase,
            };
        }
    }
}
