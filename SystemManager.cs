using System.IO;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.UsersClasses;
using CS251_A3_ToffeeShop.BalanceClasses;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text.Json;


namespace CS251_A3_ToffeeShop
{
    public class SystemManager
    {
        private Catalogue catalogue = new Catalogue();
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private List<Order> orderList = new List<Order>();
        private List<Voucher> voucherList = new List<Voucher>();
        private User? currentUser;
        private int userInput;

        public void SystemRun()
        {
            // Load Data at the start of the program
            LoadData();


            // Login/Register
            Console.WriteLine("\nWelcome to Toffee Shop!\n");

            do
            {
                Console.WriteLine("\n1) Browse Catalogue\n2) Register\n3) Login\n4) Exit");
                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput)
                {
                    case 1:
                        catalogue.DisplayCatalogue();
                        break;
                    case 2:
                        // Register a new User
                        RegisterUser();
                        break;
                    case 3:
                        // Login
                        if (!LoginUser()) break;
                        // Check if user is found in DB

                        // if his type is Admin
                        if (currentUser is Admin)
                        {
                            AdminSystem();
                        }

                        // if his type is customer
                        if (currentUser is Customer)
                        {
                            CustomerSystem();
                        }
                        break;
                    case 4:
                        Console.WriteLine("Closing Program!\n");
                        break;
                    default:
                        Console.WriteLine("Please choose a valid number!\n");
                        break;
                }
            } while (userInput != 4);

            // Save Data before Closing the program
            SaveData();
        }
        public void OrderInterface() {
            if (currentUser == null) return;
            int orderInput;
            do {
                ((Customer)currentUser).PrintOrders();
                System.Console.WriteLine("");
                System.Console.WriteLine("1) Cancel Order.\n2) Go to Menu.");
                int.TryParse(Console.ReadLine(), out orderInput);
                switch(orderInput) {
                    case 1:
                        int orderNumber;
                        Console.Write("Enter an Order Number(Press 0 to Cancel): ");
                        int.TryParse(Console.ReadLine(),out orderNumber);
                        if (orderNumber < 0 || orderNumber > ((Customer)currentUser).GetOrderHistory().Count){
                            Console.WriteLine("Order is Invalid");
                            break;
                        }
                        else if (orderNumber == 0){
                            return;
                        }
                        ((Customer)currentUser).GetOrderHistory()[orderNumber - 1].SetOrderStatue(OrderState.Canceled);
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Invalid Input! Try Again!");
                        break;
                }
            }while (orderInput != 2);

        private void CustomerSystem() {
            if (currentUser == null) return;

            do {
                Console.WriteLine("\n1) Browser Catalogue.\n2) Add Item to Shopping Cart.\n3) Review Shopping Cart.\n4) Log out.");
                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput)
                {
                    case 1:     // Browser Catalogue
                        catalogue.DisplayCatalogue();
                        break;
                    case 2:     // Adding Item To Shopping Cart
                        catalogue.DisplayCatalogue();
                        int choice, quantity;

                        Console.Write("Pick an Item to Add: ");
                        int.TryParse(Console.ReadLine(), out choice);
                        if (choice <= 0 || choice > catalogue.GetProductList().Count)
                        {
                            Console.WriteLine("Invalid choice!\nPlease pick an item from the list!");
                            break;
                        }

                        Console.Write("How many do you want to Add: ");
                        int.TryParse(Console.ReadLine(), out quantity);
                        if (quantity <= 0 || quantity > 50)
                        {
                            Console.WriteLine("Invalid quantity!\nQuantity must be from 1 to 50!");
                            break;
                        }

                        ((Customer)currentUser).GetShoppingCart().AddItem(catalogue.GetProductList()[choice - 1], quantity);
                        break;
                    case 3:
                        ShoppingInterface();
                        break;
                    case 4:
                        OrderInterface();
                        break;
                    case 5:     // Logging out
                        Console.WriteLine("Logged out Successfully!");
                        break;
                    default:    // Invalid Input
                        Console.WriteLine("Please choose a valid number!\n");
                        break;
                }
            } while (userInput != 5);
        }

        private void AdminSystem()
        {
            if (currentUser == null) return;

            do
            {
                Console.WriteLine("\n1) Update Catalogue.\n2) Update Vouchers.\n3) Update LoyalityPoints.\n4) Cancel Order.\n5) Update Order.\n6) Suspend Customer. \n7) Log out.");
                int.TryParse(Console.ReadLine(), out userInput);
                int i = 1;
                int choice;
                if (userInput < 1 || userInput > 7)
                {
                    AdminSystem();
                    return;
                }
                else
                {
                    // NEED TO ADD Validation checks on choices of each case //FIXED
                    switch (userInput)
                    {
                        case 1:
                            ((Admin)(currentUser)).UpdateCatalogue(catalogue);
                            break;
                        case 2:
                            ((Admin)(currentUser)).UpdateVouchers(voucherList);
                            break;
                        case 3:
                            double value;
                            Console.WriteLine("Enter The New Discount Value Please: ");
                            while (!double.TryParse(Console.ReadLine(), out value))
                            {
                                Console.WriteLine("Please Enter a valid value!");
                            }
                            ((Admin)(currentUser)).UpdateLoyalityPoint(value);
                            break;
                        case 4:
                            i = 1;
                            foreach (var order in orderList)
                            {
                                Console.WriteLine($"{i++}) " + " " + order.GetOrderShoppingCart() + order.GetOrderState() + " " + order.GetType() + " " + order.GetDateTime());
                            }
                            int.TryParse(Console.ReadLine(), out choice);
                            ((Admin)(currentUser)).CancelOrder(orderList[choice - 1]);
                            break;
                        case 5:
                            i = 1;
                            foreach (var v in orderList)
                            {
                                Console.WriteLine($"{i++}) " + " " + v.GetOrderShoppingCart() + " " + v.GetOrderState() + " " + v.GetType() + " " + v.GetDateTime());
                            }
                            int.TryParse(Console.ReadLine(), out choice);
                            ((Admin)(currentUser)).UpdateOrder(orderList[choice - 1]);
                            break;
                        case 6:
                            i = 1;
                            List<Customer> customers = new List<Customer>();
                            foreach (var user in users)
                            {
                                if (user.Value is Customer)
                                {
                                    Console.WriteLine($"{i++}) " + user.Value.GetName() + " " + user.Value.GetEmail() + " " + user.Value.GetUsername() + " " + user.Value.GetPhonenumber());
                                    customers.Add((Customer)(user.Value));
                                }
                            }
                            int.TryParse(Console.ReadLine(), out choice);
                            ((Admin)(currentUser)).SuspendCustomer(customers[choice - 1]);
                            break;
                    }
                }
            } while (userInput != 7);
        }

        public void CustomerLoyalityPointsInterface()
        {
            if (currentUser == null) return;
            Console.WriteLine("Available Points: {0} TP\n-------------------\n1) Redeem.\n2) Cancel.", ((Customer)currentUser).GetLoyalityPoints().GetPoints());
            int userInput;
            int.TryParse(Console.ReadLine(), out userInput);
            switch (userInput)
            {
                case 1:
                    int points;
                    Console.Write("Enter ammount to be redeemed: ");
                    int.TryParse(Console.ReadLine(), out points);
                    if (points > ((Customer)currentUser).GetLoyalityPoints().GetPoints())
                    {
                        System.Console.WriteLine("Points Exceeded The Limit!");
                        CustomerLoyalityPointsInterface();
                    }
                    ((Customer)currentUser).GetLoyalityPoints().RedeemPoints(points);
                    return;
                case 2:
                    return;
            }
        }

        public void CustomerVoucherInterface()
        {
            if (currentUser == null) return;
            int i = 1;
            List<Voucher> availableVoucherList = ((Customer)currentUser).GetVoucherList();

            if (availableVoucherList == null || availableVoucherList.Count <= 0)
            {
                Console.WriteLine("You don't have any Vouchers!");
                Console.WriteLine("Vouchers can be obtained by spending 50 L.E or more!");    // the amount will be changed to a variable
                return;
            }

            foreach (var voucher in availableVoucherList)
            {
                if (voucher.GetExpiryState() == false)
                {
                    Console.WriteLine((i++) + ", " + voucher.GetVoucherCode() + "   " + voucher.GetDiscountValue());
                }
            }
            Console.Write("Choose an Option: ");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            if (choice <= 0 || choice > availableVoucherList.Count)
            {
                Console.WriteLine("Invalid choice!\nPlease pick an item from the list!");
                return;
            }

            ((Customer)currentUser).GetShoppingCart().ApplyVoucher(availableVoucherList[choice - 1]);
        }

        public void ShoppingInterface()
        {
            if (currentUser == null) return;

            int userInput;
            do
            {
                Console.WriteLine("//---------------//Shopping Cart//---------------//");
                ((Customer)currentUser).GetShoppingCart().PrintItems();
                Console.WriteLine("1) Apply LoyalityPoints\n2) Apply Vouchers\n3) Update Quantity\n4) Clear\n5) Check Out\n6) Cancel");
                int.TryParse(Console.ReadLine(), out userInput);
                switch (userInput)
                {
                    case 1:
                        CustomerLoyalityPointsInterface();
                        break;
                    case 2:
                        CustomerVoucherInterface();
                        break;
                    case 3:
                        ((Customer)currentUser).GetShoppingCart().Updateitems();
                        break;
                    case 4:
                        ((Customer)currentUser).GetShoppingCart().ClearCart();
                        Console.WriteLine("Cart Cleared!");
                        break;
                    case 5:
                        ((Customer)currentUser).CheckOut();
                        ((Customer)currentUser).GetShoppingCart().ClearCart();
                        Console.WriteLine("Order has been Processed!");
                        break;
                    case 6:
                        ((Customer)currentUser).GetShoppingCart().RevertChanges();
                        break;
                    default:
                        System.Console.WriteLine("Invalid Input! Try Again!");
                        break;
                }
            } while (userInput != 6 && userInput != 5 && userInput != 4);
        }

        private void RegisterUser() {
            string? name = string.Empty;
            string? username, password, emailAdress;
            string city = string.Empty;
            string street = string.Empty;
            string buildingNo = string.Empty;
            Regex usernamePattern = new Regex("^[a-zA-Z0-9_-]+$");
            Regex passwordPattern = new Regex("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[$#&*%^])[A-Za-z\\d$#&*%^]{8,}$");
            Regex emailPattern = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");

            bool correctInput;

            // Take Name
            if (!TakeRegistrationInputNoRegex(ref name, "Name")) return;

            // Take Username
            bool found;
            do
            {
                found = false;
                correctInput = false;
                Console.Write("Please Enter your Username (Enter 0 to cancel Registration): ");
                username = Console.ReadLine();
                if (username == "0")
                {
                    Console.WriteLine("Registration Canceled!");
                    return;
                }

                if (string.IsNullOrEmpty(username) || !usernamePattern.IsMatch(username))
                {
                    Console.WriteLine("Invalid Username!\nUsername should consist of letters, numbers, _, -");
                }
                else
                {
                    correctInput = true;
                    if (users.ContainsKey(username))
                    {
                        Console.WriteLine("This username is already taken!");
                        found = true;
                    }
                }
            } while (found || !correctInput || string.IsNullOrEmpty(username));

            // Take Password
            do
            {
                correctInput = false;
                Console.Write("Please Enter your password (Enter 0 to cancel Registration): ");
                password = Console.ReadLine();
                if (password == "0")
                {
                    Console.WriteLine("Registration Canceled!");
                    return;
                }

                if (string.IsNullOrEmpty(password) || !passwordPattern.IsMatch(password))
                {
                    Console.WriteLine("Invalid password!");
                    Console.WriteLine("Password must consist of letters, numbers and one of [$ # & * % ^] and be at least 8 characters long");
                }
                else
                {
                    correctInput = true;
                }
            } while (!correctInput || string.IsNullOrEmpty(password));

            // Take Email
            do
            {
                correctInput = false;
                Console.Write("Please Enter your Email Address (Enter 0 to cancel Registration): ");
                emailAdress = Console.ReadLine();
                if (emailAdress == "0")
                {
                    Console.WriteLine("Registration Canceled!");
                    return;
                }

                if (string.IsNullOrEmpty(emailAdress) || !emailPattern.IsMatch(emailAdress))
                {
                    Console.WriteLine("Invalid Email Address!");
                    Console.WriteLine("Please enter a valid Email Address!");
                }
                else
                {
                    correctInput = true;
                }
            } while (!correctInput || string.IsNullOrEmpty(emailAdress));

            // Take City
            if (!TakeRegistrationInputNoRegex(ref city, "City")) return;

            // Take Street
            if (!TakeRegistrationInputNoRegex(ref street, "Street")) return;

            // Take Building Number
            if (!TakeRegistrationInputNoRegex(ref buildingNo, "Building Number")) return;
            Address newaddress = new Address(street, city, buildingNo);
            User customer = new Customer(name, username, password, emailAdress, newaddress);
            users.Add(username, customer);
            Console.WriteLine("Registered Successfully!");
        }

        private bool LoginUser()
        {
            string? username;
            string? password;
            bool correctInput;

            do
            {
                correctInput = false;
                Console.WriteLine("Please enter your Username (Enter 0 to cancel Login): ");
                username = Console.ReadLine();
                Console.WriteLine("Please enter your Password (Enter 0 to cancel Login): ");
                password = Console.ReadLine();

                if (username == "0" || password == "0")
                {
                    Console.WriteLine("Login Canceled!");
                    return false;
                }

                if (string.IsNullOrEmpty(username))
                {
                    Console.WriteLine("Username cannot be empty!");
                    continue;
                }

                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Password cannot be empty!");
                    continue;
                }

                if (!users.ContainsKey(username))
                {
                    Console.WriteLine("User does not exist!");
                }
                else
                {
                    if (users[username].GetPassword() != password)
                    {
                        Console.WriteLine("Incorrect Password!");
                    }
                    else
                    {
                        correctInput = true;
                    }
                }
            } while (!correctInput);

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Login Failed!");
                return false;
            }

            if (users[username] is Customer && ((Customer)users[username]).GetCustomerState() == CustomerState.inactive)
            {
                Console.WriteLine("Suspended User Account!");
                return false;
            }

            currentUser = users[username];
            Console.WriteLine("Logged in Successfully!");
            Console.WriteLine($"Welcome {currentUser.GetName()}!");
            return true;
        }

        private bool TakeRegistrationInputNoRegex(ref string variable, string strname)
        {
            bool correctInput;
            string? input;
            do
            {
                correctInput = false;
                Console.Write($"Please Enter your {strname} (Enter 0 to cancel Registration): ");
                input = Console.ReadLine();
                if (input == "0")
                {
                    Console.WriteLine("Registration Canceled!");
                    return false;
                }

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine($"Invalid {strname}!");
                    Console.WriteLine($"{strname} Cannot be Empty!");
                }
                else
                {
                    correctInput = true;
                }
            } while (!correctInput || string.IsNullOrEmpty(input));

            variable = input;

            return true;
        }

        private void LoadData()
        {
            catalogue.LoadCatalogueData("./Items/Data.json");
        }

        private void SaveData()
        {
            catalogue.SaveCatalogueData("./Items/Data.json");

            List<CustomerData> customerDataList = new List<CustomerData>();
            List<AdminData> AdminDataList = new List<AdminData>();

            foreach (KeyValuePair<string, User> user in users)
            {
                if (user.Value is Customer)
                {
                    customerDataList.Add(SaveCustomerData((Customer)user.Value));
                }

                if (user.Value is Admin)
                {
                    AdminDataList.Add(SaveAdminData((Admin)user.Value));
                }
            }

            SaveDataLists(customerDataList, "./UsersClasses/Customers.json");
            SaveDataLists(AdminDataList, "./UsersClasses/Admins.json");
        }

        private CustomerData SaveCustomerData(Customer customer)
        {
            CustomerData customerData = new CustomerData();
            customerData.orders = new List<OrderData>();
            customerData.vouchers = new List<VoucherData>();
            customerData.name = customer.GetName();
            customerData.username = customer.GetUsername();
            customerData.password = customer.GetPassword();
            customerData.phone = customer.GetPhonenumber();
            customerData.email = customer.GetEmail();

            // Change orders to OrderData
            foreach (Order order in customer.GetOrderHistory())
            {
                OrderData orderData = new OrderData();
                orderData.orderState = order.GetOrderState();

                ShoppingCartData cartData = new ShoppingCartData();
                foreach (KeyValuePair<Product, int> product in order.GetShoppingCart().GetProductList()) {
                    ProductData data = new ProductData();
                    data.name = product.Key.GetName();
                    data.category = product.Key.GetCategory();
                    data.description = product.Key.GetDescription();
                    data.brand = product.Key.GetBrand();
                    data.price = product.Key.GetPrice();
                    data.discountPrice = product.Key.GetDiscountPrice();

                    cartData.items.Add(new KeyValuePair<ProductData, int>(data, product.Value));
                }
                cartData.totalCost = order.GetOrderShoppingCart().CalculateTotalPrice();
                orderData.shoppingCartData = cartData;

                orderData.deliveryAddress = order.GetDeliveryAddress();
                orderData.dateTime = order.GetDateTime();

                customerData.orders.Add(orderData);
            }

            //Change Vouchers to VoucherData
            foreach (Voucher voucher in customer.GetVoucherList())
            {
                VoucherData voucherData = new VoucherData();
                voucherData.voucherCode = voucher.GetVoucherCode();
                voucherData.discountValue = voucher.GetDiscountValue();
                voucherData.isExpired = voucher.GetExpiryState();

                customerData.vouchers.Add(voucherData);
            }

            customerData.loyalityPoints = customer.GetLoyalityPoints().GetPoints();
            customerData.address = customer.GetAddress();
            customerData.customerState = customer.GetCustomerState();

            return customerData;
        }

        private AdminData SaveAdminData(Admin admin)
        {
            AdminData adminData = new AdminData();

            adminData.name = admin.GetName();
            adminData.username = admin.GetUsername();
            adminData.password = admin.GetPassword();
            adminData.phone = admin.GetPhonenumber();
            adminData.email = admin.GetEmail();

            return adminData;
        }

        private void SaveDataLists<T>(List<T> list, string file)
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                string json = JsonSerializer.Serialize(list.ToArray());

                // write the serialized Json to the file
                writer.Write(json);
            }
        }
    }
}