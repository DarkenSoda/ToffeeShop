using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.Items;

namespace CS251_A3_ToffeeShop {
    public class MainClass {
        public static void Main(string[] args) {
            Product product1 = new Product("Stormish","safashkash",5);
            Product product2 = new Product("Norm","safashkash",6);
            Product product3 = new Product("form","safashkash",6);
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(product1,2);
            shoppingCart.AddItem(product2,5);
            shoppingCart.Updateitems();
        }
    }
}