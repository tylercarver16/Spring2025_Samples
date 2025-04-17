using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private Dictionary<string, List<Item>> carts = new();
        private string activeCart = "Default";

        public List<Item> CartItems
        {
            get
            {
                if (!carts.ContainsKey(activeCart))
                    carts[activeCart] = new List<Item>();
                return carts[activeCart];
            }
        }

        public static ShoppingCartService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartService();
                }
                return instance;
            }
        }

        private static ShoppingCartService? instance;

        private ShoppingCartService()
        {
            carts["Default"] = new List<Item>();
        }

        public void SetActiveCart(string cartName)
        {
            activeCart = cartName;
            if (!carts.ContainsKey(cartName))
            {
                carts[cartName] = new List<Item>();
            }
        }

        public Item? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Quantity == 0)
                return null;

            existingInvItem.Quantity--;

            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                var newItem = new Item(item) { Quantity = 1 };
                CartItems.Add(newItem);
            }
            else
            {
                existingItem.Quantity++;
            }

            return existingInvItem;
        }

        public Item? ReturnItem(Item? item)
        {
            if (item?.Id <= 0)
                return null;

            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if (inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                }
                else
                {
                    inventoryItem.Quantity++;
                }
            }

            return itemToReturn;
        }

        public void ClearCart()
        {
            if (carts.ContainsKey(activeCart))
                carts[activeCart].Clear();
        }
    }
}
