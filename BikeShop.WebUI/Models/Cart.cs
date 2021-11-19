using BikeShop.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BikeShop.WebUI.Models
{
    public class Cart
    {
        private List<CartItem> cartItems = new List<CartItem>();
        public IEnumerable<CartItem> CartItems { get => CurrentCategory == null ? cartItems : cartItems.Where(c => c.Product.Category.Name == CurrentCategory); set => cartItems.AddRange(value); }
        public string CurrentCategory { get; set; }
        public void AddItem(Product product, int quantity)
        {
            CartItem cartItem = cartItems.FirstOrDefault(g => g.Product.Id == product.Id);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                if (cartItem.Quantity == 0)
                    RemoveItem(cartItem.Product);
            }
            else
                cartItems.Add(new CartItem { Product = product, Quantity = quantity });
        }
        public void RemoveItem(Product product)
        {
            cartItems.RemoveAll(c => c.Product.Id == product.Id);
        }
        public double CalculateTotalValue()
        {
            return cartItems.Sum(c => c.Product.Price * c.Quantity);
        }
        public void Clear()
        {
            cartItems.Clear();
        }
    }
    public class CartItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }

}
