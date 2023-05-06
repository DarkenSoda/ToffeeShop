using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.BalanceClasses;
using CS251_A3_ToffeeShop.Items;

namespace CS251_A3_ToffeeShop.UsersClasses {
    public enum CustomerState {
        active, inactive
    }

    public class Customer : User {
        private CustomerState customerState;
        private List<Order> orderHistory = new List<Order>();
        private ShoppingCart shoppingCart = new ShoppingCart();
        protected List<Voucher> voucherList = new List<Voucher>();
        private LoyalityPoints loyalityPoints = new LoyalityPoints();
        // List<PaymentMethodstrategy>? PaymentMethods;
        Address address;
        public Customer(string name, string userName, string password, string emailAdress, Address caddress) : base(name, userName, password, emailAdress) {
            address = caddress;
            customerState = CustomerState.active;
        }
        public ShoppingCart GetShoppingCart() {
            return shoppingCart;
        }
        public List<Order> GetOrderHistory() {
            return orderHistory;
        }
        public List<Voucher> GetVoucherList() {
            return voucherList;
        }
        public Address GetAddress() {
            return address;
        }
        public LoyalityPoints GetLoyalityPoints() {
            return loyalityPoints;
        }
        public void UpdateShoppingCart(ShoppingCart shoppingCart) {
            shoppingCart.PrintItems();
            Console.WriteLine("1-Add New Order.");
            Console.WriteLine("2-Remove Order.");
            Console.WriteLine("3-Edit Order quantity.");
            Console.WriteLine("4-Clear Cart.");
            int y;
            Console.WriteLine("Enter Your Choice Please: ");
            y = Convert.ToInt32(Console.ReadLine());
            if (y < 1 || y > 4) {
                UpdateShoppingCart(shoppingCart);
            } else {
                if (y == 1) {
                    Catalogue _catalogue = new Catalogue();
                    _catalogue.DisplayCatalogue();
                    int n;
                    Console.WriteLine("Enter Your Choice Please: ");
                    n = Convert.ToInt32(Console.ReadLine());
                    int x;
                    Console.WriteLine("Enter The Quantity: ");
                    x = Convert.ToInt32(Console.ReadLine());
                    shoppingCart.AddItem(_catalogue.GetProductList()[n - 1], x);
                } else if (y == 2) {
                    shoppingCart.PrintItems();
                    int n;
                    Console.WriteLine("Enter Your Choice Please: ");
                    n = Convert.ToInt32(Console.ReadLine());
                    //shoppingCart.RemoveItem(shoppingCart.GetProductList()[n-1].key);
                } else if (y == 3) {
                    shoppingCart.PrintItems();
                    int n;
                    Console.WriteLine("Enter Your Choice Please: ");
                    n = Convert.ToInt32(Console.ReadLine());
                    //shoppingCart.GetProductList()[n-1].key;
                }
            }
        }
        public void SetCustomerState(CustomerState _customerState) {
            customerState = _customerState;
        }
        public void CheckOut() {
            Console.WriteLine("Order Total Price= ");
            shoppingCart.CalculateTotalPrice();
        }
        public CustomerState GetCustomerState() {
            return customerState;
        }

    }

    public struct CustomerData {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public List<OrderData> orders { get; set; }
        public List<VoucherData> vouchers { get; set; }
        public int loyalityPoints { get; set; }
        public Address address { get; set; }
        public CustomerState customerState { get; set; }
    }
}