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
                Console.WriteLine("\n1) Browse Catalogue\n2) Register\n3) Log in\n4) I forgot my Password!\n5) Exit");
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
                        ForgotPassword();
                        break;
                    case 5:
                        Console.WriteLine("Closing Program!\n");
                        break;
                    default:
                        Console.WriteLine("Please choose a valid number!\n");
                        break;
                }
            } while (userInput != 5);

            // Save Data before Closing the program
            SaveData();
        }

        private void CustomerSystem()
        {
            if (currentUser == null) return;

            do
            {
                Console.WriteLine("\n1) Browser Catalogue.\n2) Add Item to Shopping Cart.\n3) Review Shopping Cart.\n4) Review Orders.\n5) Authenticate.\n6) Log out.");
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
                    case 5:
                        if (Authentication.Authenticate(((Customer)currentUser).GetEmail()))
                        {
                            ((Customer)currentUser).SetAuthentication(true);
                        }
                        break;
                    case 6:     // Logging out
                        Console.WriteLine("Logged out Successfully!");
                        break;
                    default:    // Invalid Input
                        Console.WriteLine("Please choose a valid number!\n");
                        break;
                }
            } while (userInput != 6);
        }

        private void AdminSystem()
        {
            if (currentUser == null) return;

            do
            {
                Console.WriteLine("\n1) Update Catalogue.\n2) Update Vouchers.\n3) Update LoyalityPoints.\n4) Cancel Order.\n5) Update Order.\n6) Suspend Customer.\n7) Authenicate.\n8) Log out.");
                int.TryParse(Console.ReadLine(), out userInput);
                int i = 1;
                int choice;
                if (userInput < 1 || userInput > 8)
                {
                    AdminSystem();
                    return;
                }
                else
                {
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
                            if(orderList == null || orderList.Count <=0){
                                Console.WriteLine("No Orders Found");
                                break;
                            }
                            i = 1;
                            System.Console.WriteLine("{}-----------------{[ Orders ]}-----------------{}");
                            foreach (var order in orderList) {
                                Console.WriteLine("[ Order {0}  {1} ]----- {2} ----", i++, order.GetDateTime(), order.GetOrderState());
                                order.GetOrderShoppingCart().PrintItems();
                                System.Console.WriteLine(" Address: {0}\n", order.GetDeliveryAddress().GetAddress());
                            }
                            Console.Write("Enter The Order You Want To Cancel Please(enter 0 if you want to cancel): ");
                            while(!int.TryParse(Console.ReadLine(), out choice)){
                                Console.WriteLine("Invalid Input Try Again!");
                            }
                            if(choice <= 0){
                                break;
                            }

                            ((Admin)(currentUser)).CancelOrder(orderList[choice - 1]);
                            break;
                        case 5:
                            i = 1;
                            if(orderList == null || orderList.Count <=0){
                                Console.WriteLine("No Orders Found");
                                break;
                            }
                            i = 1;
                            System.Console.WriteLine("{}-----------------{[ Orders ]}-----------------{}");
                            foreach (var order in orderList) {
                                Console.WriteLine("[ Order {0}  {1} ]----- {2} ----", i++, order.GetDateTime(), order.GetOrderState());
                                order.GetOrderShoppingCart().PrintItems();
                                System.Console.WriteLine(" Address: {0}\n", order.GetDeliveryAddress().GetAddress());
                            }
                            Console.Write("Enter The Order You Want To Update Please(enter 0 if you want to cancel): ");
                            while(!int.TryParse(Console.ReadLine(), out choice)){
                                Console.WriteLine("Invalid Input Try Again!");
                            }
                            if(choice <= 0){
                                break;
                            }
                            ((Admin)(currentUser)).UpdateOrder(orderList[choice - 1]);
                            break;
                        case 6:
                            i = 1;
                            List<Customer> customers = new List<Customer>();
                            foreach (var user in users)
                            {
                                if (user.Value is Customer)
                                {
                                    customers.Add((Customer)(user.Value));
                                    Console.WriteLine($"{i}) " + customers[i-1].GetName() + " " + customers[i-1].GetEmail() + " " + customers[i-1].GetUsername() + " " + customers[i-1].GetPhonenumber());
                                    i++;
                                }
                            }
                            if (customers == null || customers.Count <= 0)
                            {
                                Console.WriteLine("NO Customers Found!");
                                break;
                            }
                            Console.Write("Enter The User You Want To Suspend Please (Enter 0 to cancel): ");
                            while(!int.TryParse(Console.ReadLine(), out choice)|| choice < 0 || choice >customers.Count){
                                Console.WriteLine("Invalid Input Try Again!");
                            }
                            if(choice == 0){
                                break;
                            }
                            ((Admin)(currentUser)).SuspendCustomer(customers[choice - 1]);
                            Console.WriteLine("Customer Suspended.");
                            break;
                        case 7:
                            if (Authentication.Authenticate(((Admin)currentUser).GetEmail()))
                            {
                                ((Admin)currentUser).SetAuthentication(true);
                            }
                            break;
                        case 8:
                            Console.WriteLine("Process Canceled!");
                            break;
                    }
                }
            } while (userInput != 8);
        }

        public void OrderInterface()
        {
            if (currentUser == null) return;
            int orderInput;
            do
            {
                ((Customer)currentUser).PrintOrders();
                System.Console.WriteLine("");
                System.Console.WriteLine("1) Cancel Order.\n2) Re-Order.\n3) Go to Menu.");
                int.TryParse(Console.ReadLine(), out orderInput);
                switch (orderInput)
                {
                    case 1:
                        int orderNumber;
                        Console.Write("Enter an Order Number(Press 0 to Cancel): ");
                        int.TryParse(Console.ReadLine(), out orderNumber);
                        if (orderNumber < 0 || orderNumber > ((Customer)currentUser).GetOrderHistory().Count)
                        {
                            Console.WriteLine("Order is Invalid");
                            break;
                        }
                        else if (orderNumber == 0)
                        {
                            return;
                        }
                        ((Customer)currentUser).GetOrderHistory()[orderNumber - 1].SetOrderStatue(OrderState.Canceled);
                        break;
                    case 2:
                        if (((Customer)currentUser).GetOrderHistory().Count > 0)
                        {
                            ((Customer)currentUser).ReOrder(catalogue);
                        }
                        else
                        {
                            Console.WriteLine("List is empty!");
                        }
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Invalid Input! Try Again!");
                        break;
                }
            } while (orderInput != 3);

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
            var availableVoucherList = ((Customer)currentUser).GetVoucherList().Where(v => !v.GetExpiryState() && !((Customer)currentUser).GetShoppingCart().GetAppliedVouchers().Contains(v)).ToList();
            if (availableVoucherList == null || availableVoucherList.Count <= 0)
            {
                Console.WriteLine("You don't have any Vouchers!");
                Console.WriteLine("Vouchers can be obtained by spending 50 L.E or more!");    // the amount will be changed to a variable
                return;
            }
            foreach (var voucher in availableVoucherList) {
                if (((Customer)currentUser).GetShoppingCart().GetAppliedVouchers().Contains(voucher)) {
                    availableVoucherList.Remove(voucher);
                }
                Console.WriteLine((i++) + ", " + voucher.GetVoucherCode() + "   " + voucher.GetDiscountValue());
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

                Console.WriteLine("{}---------------{[ Shopping Cart ]}---------------{}");
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
                        ((Customer)currentUser).GetShoppingCart().GetAppliedVouchers().Clear();
                        Console.WriteLine("Cart Cleared!");
                        break;
                    case 5:
                        Random randInt = new Random();
                        if (((Customer)currentUser).GetShoppingCart().GetProductList().Count == 0)
                        {
                            Console.WriteLine("Cart is Empty!");
                            break;
                        }

                        if (!((Customer)currentUser).CheckOut(orderList)) {
                            break;
                        }
                        if (voucherList.Count > 0)
                        {
                            ((Customer)currentUser).AddVoucher(voucherList[randInt.Next(0, voucherList.Count)]);
                        }

                        // Redeem every voucher in the applied list in the checkout.
                        foreach(var voucher in ((Customer)currentUser).GetShoppingCart().GetAppliedVouchers()) {
                            voucher.SetDiscountValue(voucher.GetDiscountValue() - ((Customer)currentUser).GetShoppingCart().GetTotalCost());
                            if (voucher.GetDiscountValue() < 0) {
                                voucher.SetDiscountValue(0);
                            }
                            voucher.RedeemVoucher();
                        }
                        ((Customer)currentUser).GetLoyalityPoints().AddPoints(randInt.Next(1, 101));
                        ((Customer)currentUser).GetShoppingCart().ClearCart();
                        ((Customer)currentUser).GetShoppingCart().GetAppliedVouchers().Clear();
                        return;
                    case 6:
                        ((Customer)currentUser).GetShoppingCart().RevertChanges();
                        ((Customer)currentUser).GetShoppingCart().GetAppliedVouchers().Clear();
                        break;
                    default:
                        System.Console.WriteLine("Invalid Input! Try Again!");
                        break;
                }
            } while (userInput != 6 && userInput != 4);
        }

        private void RegisterUser()
        {
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
                    // Send OTP to email to check if it's valid
                    while (true)
                    {
                        if (Authentication.Authenticate(emailAdress))
                        {
                            correctInput = true;
                            break;
                        }

                        int input;
                        do
                        {
                            Console.WriteLine("Email Verification Failed!");
                            Console.WriteLine("1) Resend code\n2) Change Email\n3) Cancel Registration\n4) Bypass Verification (ONLY WHEN OFFLINE OR WANT A FAST REGISTRATION)");
                            if (!int.TryParse(Console.ReadLine(), out input))
                            {
                                Console.WriteLine("Invalid Input!");
                            }
                        } while (!Enumerable.Range(1, 4).Contains(input));

                        if (input == 2) break;

                        if (input == 3) return;

                        // ONLY WHEN OFFLINE OR WANT A FAST REGISTRATION
                        if (input == 4)
                        {
                            correctInput = true;
                            break;
                        }
                    }

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
                Console.WriteLine("Please enter your Username (Enter 0 to cancel Log in): ");
                username = Console.ReadLine();
                Console.WriteLine("Please enter your Password (Enter 0 to cancel Log in): ");
                password = Console.ReadLine();

                if (username == "0" || password == "0")
                {
                    Console.WriteLine("Log in Canceled!");
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
                Console.WriteLine("Log in Failed!");
                return false;
            }

            if (users[username] is Customer && ((Customer)users[username]).GetCustomerState() == CustomerState.inactive)
            {
                Console.WriteLine("Suspended User Account!");
                return false;
            }

            // Authenticate user if 2FA is enabled
            if (users[username].GetAuthentication())
            {
                int bypass;
                Console.WriteLine("Enter 1 Bypass Verification (ONLY WHEN OFFLINE OR WANT A FAST REGISTRATION)");
                if (int.TryParse(Console.ReadLine(), out bypass) && bypass == 1)
                {
                    Console.WriteLine("Bypassed Authentication!");
                }
                else
                {
                    if (!Authentication.Authenticate(users[username].GetEmail()))
                    {
                        Console.WriteLine("Log in Failed!");
                        return false;
                    }
                }
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

        public void ForgotPassword()
        {
            // Take username from customer then check if it's found first
            Console.Write("Please Enter your username (Enter 0 to cancel Registration): ");
            string? username = Console.ReadLine();
            if (username == "0")
            {
                Console.WriteLine("Process Canceled!");
                return;
            }

            if (string.IsNullOrEmpty(username) || !users.ContainsKey(username))
            {
                Console.WriteLine("User not found!");
                return;
            }

            Regex passwordPattern = new Regex("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[$#&*%^])[A-Za-z\\d$#&*%^]{8,}$");
            string? password;
            bool correctInput;

            if (Authentication.Authenticate(users[username].GetEmail()))
            {
                do
                {
                    correctInput = false;
                    Console.Write("Please Enter your new Password (Enter 0 to cancel Registration): ");
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

                users[username].SetPassword(password);
            }
            else
            {
                Console.WriteLine("Process Failed!");
            }
        }

        private void LoadData()
        {
            catalogue.LoadCatalogueData("./Items/Data.json");
            LoadCustomerData("./UsersClasses/Customers.json");
            LoadAdminData("./UsersClasses/Admins.json");
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
            customerData.loyalityPoints = customer.GetLoyalityPoints().GetPoints();
            customerData.address = customer.GetAddress();
            customerData.customerState = customer.GetCustomerState();
            customerData.isAuthenticated = customer.GetAuthentication();

            // Change orders to OrderData
            foreach (Order order in customer.GetOrderHistory())
            {
                OrderData orderData = new OrderData();
                orderData.orderState = order.GetOrderState();

                ShoppingCartData cartData = new ShoppingCartData();
                cartData.items = new List<KeyValuePair<ProductData, int>>();
                foreach (KeyValuePair<Product, int> product in order.GetOrderShoppingCart().GetProductList())
                {
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
            adminData.isAuthenticated = admin.GetAuthentication();

            return adminData;
        }

        private void SaveDataLists<T>(List<T> list, string file)
        {
            if (!file.EndsWith(".json"))
            {
                Console.WriteLine("Failed To Open File!");
                return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    string json = JsonSerializer.Serialize(list.ToArray());

                    // write the serialized Json to the file
                    writer.Write(json);
                }
            }
            catch
            {
                Console.WriteLine("Failed to Save User Data!");
            }
        }

        private void LoadCustomerData(string file)
        {
            // Return if can't Open file
            if (!file.EndsWith(".json"))
            {
                Console.WriteLine("Failed To Open File!");
                return;
            }
            try
            {
                // Open in Read mode
                using (StreamReader reader = new StreamReader(file))
                {
                    // Read All File
                    string json = reader.ReadToEnd();

                    // Put all data in CustomerData Struct
                    List<CustomerData>? customerDataList = JsonSerializer.Deserialize<List<CustomerData>>(json);

                    // Return If no Items in file
                    if (customerDataList == null || customerDataList.Count <= 0) return;

                    // Loop on each struct and convert it to Customer class
                    // To insert in users list
                    foreach (CustomerData customerData in customerDataList)
                    {
                        // take main customer data
                        string name = customerData.name;
                        string username = customerData.username;
                        string password = customerData.password;
                        string phone = customerData.phone;
                        string email = customerData.email;
                        Address address = customerData.address;

                        // create new customer
                        Customer customer = new Customer(name, username, password, email, address);

                        // Check if customer has phonenumber
                        if (!string.IsNullOrEmpty(phone))
                        {
                            customer.SetPhonenumber(phone);
                        }

                        // Convert OrderData to Order and add it to customer's orderList
                        foreach (OrderData orderData in customerData.orders)
                        {
                            // Convert Shopping 
                            ShoppingCart shoppingCart = new ShoppingCart();
                            foreach (KeyValuePair<ProductData, int> productData in orderData.shoppingCartData.items)
                            {
                                Product product = new Product(productData.Key.name, productData.Key.category, productData.Key.price);
                                if (!string.IsNullOrEmpty(productData.Key.description)) product.SetDescription(productData.Key.description);
                                if (!string.IsNullOrEmpty(productData.Key.brand)) product.SetBrand(productData.Key.brand);
                                if (productData.Key.discountPrice != 0) product.SetDiscountPrice(productData.Key.discountPrice);

                                shoppingCart.AddItem(product, productData.Value);
                            }
                            shoppingCart.SetTotalCost(orderData.shoppingCartData.totalCost);

                            // Create new Order with shopping cart and add it to OrderHistory
                            Order order = new Order(shoppingCart, orderData.deliveryAddress);
                            order.SetOrderStatue(orderData.orderState);
                            order.SetDateTime(orderData.dateTime);
                            customer.GetOrderHistory().Add(order);
                        }

                        // Convert VoucherData to Voucher and add it to customer's voucherList
                        foreach (VoucherData voucherData in customerData.vouchers)
                        {
                            Voucher voucher = new Voucher(voucherData.discountValue, voucherData.isExpired);

                            customer.GetVoucherList().Add(voucher);
                        }

                        // Get LoyalityPoints
                        customer.GetLoyalityPoints().AddPoints(customerData.loyalityPoints);

                        // Get CustomerState
                        customer.SetCustomerState(customerData.customerState);

                        // Get Authentication State
                        customer.SetAuthentication(customerData.isAuthenticated);

                        // Insert in users list
                        users.Add(username, customer);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to Load Admin Data!");
            }
        }

        private void LoadAdminData(string file)
        {
            // Return if can't Open file
            if (!file.EndsWith(".json"))
            {
                Console.WriteLine("Failed To Open File!");
                return;
            }
            try
            {
                // Open in Read mode
                using (StreamReader reader = new StreamReader(file))
                {
                    // Read All File
                    string json = reader.ReadToEnd();

                    // Put all data in AdminData Struct
                    List<AdminData>? adminDataList = JsonSerializer.Deserialize<List<AdminData>>(json);

                    // Return If no Items in file
                    if (adminDataList == null || adminDataList.Count <= 0) return;

                    // Loop on each struct and convert it to Admin class
                    // To insert in users list
                    foreach (AdminData adminData in adminDataList)
                    {
                        string name = adminData.name;
                        string username = adminData.username;
                        string password = adminData.password;
                        string phone = adminData.phone;
                        string email = adminData.email;
                        Admin admin = new Admin(name, username, password, email);

                        if (!string.IsNullOrEmpty(phone))
                        {
                            admin.SetPhonenumber(phone);
                        }

                        admin.SetAuthentication(adminData.isAuthenticated);

                        users.Add(username, admin);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Failed to Load Admin Data!");
            }
        }
    }
}