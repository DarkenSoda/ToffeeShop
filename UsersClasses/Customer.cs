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
        Address? address;

        Customer(string name, string userName, string password, string emailAdress,Address caddress) : base(name, userName, password, emailAdress) {
            address = caddress;
        }
        public ShoppingCart GetShoppingCart()
        {
            // Console.WriteLine("1-Add Item.");
            // Console.WriteLine("2-Remove Item.");
            // Console.WriteLine("3-Update Item.");
            // Console.WriteLine("4-Clear Cart.");
            // Console.WriteLine("Enter Your Choice: ");

            // int y = Convert.ToInt32(Console.ReadLine());
            // if (y == 1)
            // {
            //     shoppingCart.PrintItems();
            //     //string name, string category, double price = 0
            //     string? name;
            //     string? category;
            //     double? price = 0;
            //     Console.WriteLine("Enter The Product")
            //     Console.WriteLine("Enter The Product")
            //     Console.WriteLine("Enter The Product")
            //     Console.WriteLine("Enter The Product")
            //     shoppingCart.AddItem()
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