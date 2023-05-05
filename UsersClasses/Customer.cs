using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.BalanceClasses;
namespace CS251_A3_ToffeeShop.UsersClasses {
    public class Customer : User {
        public string? customerID;
        List<Order> orderHistory = new List<Order>();
        ShoppingCart shoppingCart = new ShoppingCart();
        List<Voucher> vouchers = new List<Voucher>();
        LoyalityPoints loyalityPoints = new LoyalityPoints();
        // List<PaymentMethodstrategy>? PaymentMethods;
        Address address;

        public Customer(string name, string userName, string password, string emailAdress, Address address) : base(name, userName, password, emailAdress) {
            this.address = address;
        }
        public ShoppingCart GetShoppingCart() {
            return shoppingCart;
        }
        public List<Order> GetOrderHistory() {
            return orderHistory;
        }
        public Address GetAddress() {
            return address;
        }
    }
}