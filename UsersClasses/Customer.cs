using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.BalanceClasses;
namespace CS251_A3_ToffeeShop.UsersClasses
{
    public class Customer : User
    {
        public string? customerID;
        List<Order>? orderHistory;
        ShoppingCart? shoppingCart;
        List<Voucher>? vouchers;
        LoyalityPoints? loyalityPoints;
        //List<PaymentMethodstrategy>? PaymentMethods;
        Address address;

        Customer(string name, string userName, string password, string emailAdress,Address caddress) : base(name, userName, password, emailAdress) {
            address = caddress;
        }
        public ShoppingCart GetShoppingCart()
        {
            return shoppingCart;
        }
        public List<Order> GetOrderHistory(){
            return orderHistory;
        }
        public Address GetAddress(){
            return address;
        }
    }
}