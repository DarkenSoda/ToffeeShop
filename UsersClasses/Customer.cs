using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.BalanceClasses;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.PaymentMethod;

namespace CS251_A3_ToffeeShop.UsersClasses {
    public enum CustomerState {
        active, inactive
    }

    public class Customer : User {
        private double totalMoneySpent = 0;
        private static double maxMoneySpendForVoucher = 50;
        private CustomerState customerState;
        private List<Order> orderHistory = new List<Order>();
        private ShoppingCart shoppingCart = new ShoppingCart();
        protected List<Voucher> voucherList = new List<Voucher>();
        private LoyalityPoints loyalityPoints = new LoyalityPoints();
        private IPaymentMethodStrategy? currentPaymentMethod;
        Address address;

        public Customer(string name, string userName, string password, string emailAdress, Address caddress) : base(name, userName, password, emailAdress) {
            address = caddress;
            customerState = CustomerState.active;
        }

        public void PrintOrders() {
            int i = 1;
            System.Console.WriteLine("{}-----------------{[ Orders ]}-----------------{}");
            foreach (var order in orderHistory) {
                Console.WriteLine("[ Order {0}  {1} ]----- {2} ----", i++, order.GetDateTime(), order.GetOrderState());
                order.GetOrderShoppingCart().PrintItems();
                System.Console.WriteLine(" Address: {0}\n", order.GetDeliveryAddress().GetAddress());
            }
        }

        public double GetTotalMoneySpent() {
            return totalMoneySpent;
        }

        public static double GetMaxMoneySpentForVoucher() {
            return maxMoneySpendForVoucher;
        }

        public static void SetMaxMoneySpentForVoucher(double value) {
            maxMoneySpendForVoucher = value;
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

        public void AddVoucher(Voucher voucher) {
            totalMoneySpent += shoppingCart.CalculateTotalPrice();
            if (totalMoneySpent >= maxMoneySpendForVoucher) {
                totalMoneySpent = 0;
                voucherList.Add(voucher);
            }
        }

        public void SetCustomerState(CustomerState _customerState) {
            customerState = _customerState;
        }

        public bool CheckOut() {
            int choice;
            Console.WriteLine("1) Cash.\n2) PayPal.\n3) Credit Card.\n4) Cancel Check Out!");
            int.TryParse(Console.ReadLine(), out choice);
            switch (choice) {
                case 1:
                    currentPaymentMethod = new OnDeliveryStrategy(emailAdress);
                    System.Console.WriteLine("Enter an Address Please!");
                    string? street;
                    string? city;
                    string? buildingNumber;
                    Console.WriteLine("Enter The Street Name: ");
                    street = Console.ReadLine();
                    while (string.IsNullOrEmpty(street)) {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.WriteLine("Enter The Street Name: ");
                        street = Console.ReadLine();
                    }
                    Console.WriteLine("Enter The City: ");
                    city = Console.ReadLine();
                    while (string.IsNullOrEmpty(city)) {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.WriteLine("Enter The City: ");
                        city = Console.ReadLine();
                    }
                    Console.WriteLine("Enter The Building Number: ");
                    buildingNumber = Console.ReadLine();
                    while (string.IsNullOrEmpty(buildingNumber)) {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.WriteLine("Enter The Building Number: ");
                        buildingNumber = Console.ReadLine();
                    }
                    currentPaymentMethod.ValidatePayment();
                    Address newaddress = new Address(street, city, buildingNumber);
                    Order newOrder = new Order(shoppingCart, newaddress);
                    orderHistory.Add(newOrder);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    Console.WriteLine("Check Out Canceled!");
                    return false;
                default:
                    Console.WriteLine("Invalid Input! Try Again!");
                    break;
            }
            return true;
        }

        public void ReOrder(Catalogue catalogue) {
            int userInput;
            Console.Write("Enter an item to change:");
            int.TryParse(Console.ReadLine(), out userInput);
            while (userInput < 0 || userInput > orderHistory.Count) {
                System.Console.WriteLine("Invalid Input! Try Again!");
                Console.Write("Enter an item to change:");
                int.TryParse(Console.ReadLine(), out userInput);
            }
            ShoppingCart tempShoppingCart = new ShoppingCart();
            foreach (var item in orderHistory[userInput - 1].GetOrderShoppingCart().GetProductList()) {
                string orderName = item.Key.GetName();
                foreach (var productItem in catalogue.GetProductList()) {
                    if (orderName == productItem.GetName()) {
                        tempShoppingCart.AddItem(productItem, item.Value);

                        break;
                    }
                }
            }
            if (tempShoppingCart.GetProductList().Count > 0) {
                System.Console.WriteLine("Enter an Address Please!");
                string? street;
                string? city;
                string? buildingNumber;
                Console.WriteLine("Enter The Street Name: ");
                street = Console.ReadLine();
                while (string.IsNullOrEmpty(street)) {
                    Console.WriteLine("Invalid Input Try Again! ");
                    Console.WriteLine("Enter The Street Name: ");
                    street = Console.ReadLine();
                }
                Console.WriteLine("Enter The City: ");
                city = Console.ReadLine();
                while (string.IsNullOrEmpty(city)) {
                    Console.WriteLine("Invalid Input Try Again! ");
                    Console.WriteLine("Enter The City: ");
                    city = Console.ReadLine();
                }
                Console.WriteLine("Enter The Building Number: ");
                buildingNumber = Console.ReadLine();
                while (string.IsNullOrEmpty(buildingNumber)) {
                    Console.WriteLine("Invalid Input Try Again! ");
                    Console.WriteLine("Enter The Building Number: ");
                    buildingNumber = Console.ReadLine();
                }
                Address newaddress = new Address(street, city, buildingNumber);
                Order newOrder = new Order(tempShoppingCart, newaddress);
                orderHistory.Add(newOrder);
            }
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
        public bool isAuthenticated { get; set; }
    }
}