using System;
using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.BalanceClasses;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.PaymentMethod;

/* The code defines the Customer class and CustomerData struct in the UsersClasses namespace. The
Customer class is a subclass of the User class and represents a customer of the ToffeeShop. It has
properties such as totalMoneySpent, orderHistory, shoppingCart, voucherList, loyalityPoints,
currentPaymentMethod, and address. It also has methods such as PrintOrders, GetTotalMoneySpent,
SetTotalMoneySpent, AddVoucher, CheckOut, ReOrder, and GetCustomerState. The CustomerData struct is
a data transfer object that contains information about a customer. */
namespace CS251_A3_ToffeeShop.UsersClasses {
    /* The `public enum CustomerState` is defining an enumeration type called `CustomerState` with two
    possible values: `active` and `inactive`. This enumeration type is used to represent the state
    of a customer object in the ToffeeShop system. */
    public enum CustomerState {
        active, inactive
    }

    public class Customer : User {
        /* These are the private fields of the `Customer` class in the
        `CS251_A3_ToffeeShop.UsersClasses` namespace. They represent various properties of a
        customer object in the ToffeeShop system, such as the total money spent by the customer, the
        maximum amount of money that can be spent to receive a voucher, the state of the customer
        (active or inactive), the customer's order history, shopping cart, voucher list, loyalty
        points, current payment method, and address. These fields are used to store and manipulate
        data related to a customer object. */
        private double totalMoneySpent = 0;
        private static double maxMoneySpendForVoucher = 50;
        private CustomerState customerState;
        private List<Order> orderHistory = new List<Order>();
        private ShoppingCart shoppingCart = new ShoppingCart();
        protected List<Voucher> voucherList = new List<Voucher>();
        private LoyalityPoints loyalityPoints = new LoyalityPoints();
        private IPaymentMethodStrategy? currentPaymentMethod;
        Address address;

        /* This is a constructor for the `Customer` class in the `CS251_A3_ToffeeShop.UsersClasses`
        namespace. It takes in five parameters: `name`, `userName`, `password`, `emailAdress`, and
        `caddress`. It calls the constructor of the base class `User` with the `name`, `userName`,
        `password`, and `emailAdress` parameters. It then sets the `address` field to the `caddress`
        parameter and sets the `customerState` field to `CustomerState.active`. This constructor is
        used to create a new `Customer` object with the specified properties. */
        public Customer(string name, string userName, string password, string emailAdress, Address caddress) : base(name, userName, password, emailAdress) {
            address = caddress;
            customerState = CustomerState.active;
        }

        /// This function prints out the order history, including the date, state, items, and delivery
        /// address for each order.
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

        public void SetTotalMoneySpent(double value) {
            this.totalMoneySpent = value;
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

        /// The function adds a voucher to a list if the total money spent exceeds a certain amount.
        /// 
        /// @param Voucher The Voucher parameter is an object of the Voucher class, which contains
        /// information about a discount or promotion that can be applied to a shopping cart.
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

        /// This function prompts the user to select a payment method and enter an address, then creates
        /// a new order and adds it to the system list.
        /// 
        /// @param systemList A list of orders in the system.
        /// 
        /// @return The method returns a boolean value. True if the process is successful, False if process failed
        public bool CheckOut(List<Order> systemList) {
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
                    int bybass;
                    Console.Write("Press 1 to bybass the verifcation, otherwise press any other key:");
                    int.TryParse(Console.ReadLine(), out bybass);
                    if (bybass != 1) {
                        currentPaymentMethod.ValidatePayment();
                    }
                    Address newaddress = new Address(street, city, buildingNumber);
                    Order newOrder = new Order(shoppingCart, newaddress);
                    orderHistory.Add(newOrder);
                    systemList.Add(newOrder);
                    shoppingCart.SetAppliedPoints(0);
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

        /// This function allows the user to reorder items from a shopping cart and create a new order
        /// with a new address.
        /// 
        /// @param Catalogue A class that contains a list of products available for purchase.
        public void ReOrder(Catalogue catalogue) {
            int userInput;
            Console.Write("Enter an item to change: ");
            int.TryParse(Console.ReadLine(), out userInput);
            while (userInput < 0 || userInput > orderHistory.Count) {
                Console.WriteLine("Invalid Input! Try Again!");
                Console.Write("Enter an item to change:");
                int.TryParse(Console.ReadLine(), out userInput);
            }

            ShoppingCart tempShoppingCart = new ShoppingCart();

            /* The code is iterating through the product list of a shopping cart in an order history
            object, and for each product, it is comparing its name to the names of products in a
            catalogue. If a match is found, the product and its quantity are added to a temporary
            shopping cart object. */
            foreach (var item in orderHistory[userInput - 1].GetOrderShoppingCart().GetProductList()) {
                string orderName = item.Key.GetName();
                foreach (var productItem in catalogue.GetProductList()) {
                    if (orderName == productItem.GetName()) {
                        tempShoppingCart.AddItem(productItem, item.Value);

                        break;
                    }
                }
            }

            /* The code below is checking if the product list in a shopping cart is not empty. If it is
            not empty, it prompts the user to enter an address by asking for the street name, city,
            and building number. It then creates a new Address object with the entered information
            and a new Order object with the shopping cart and the new address. Finally, it adds the
            new order to a list of order history. */
            if (tempShoppingCart.GetProductList().Count > 0) {
                Console.WriteLine("Enter an Address Please!");
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

    /* The code below is defining a C# struct called CustomerData which contains properties for storing
    customer information such as name, username, password, phone, email, total money spent, orders,
    vouchers, loyalty points, address, customer state, and authentication status. The struct is used to
    store a customer object in the database. */
    public struct CustomerData {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public double totalMoneySpent { get; set; }
        public List<OrderData> orders { get; set; }
        public List<VoucherData> vouchers { get; set; }
        public int loyalityPoints { get; set; }
        public Address address { get; set; }
        public CustomerState customerState { get; set; }
        public bool isAuthenticated { get; set; }
    }
}