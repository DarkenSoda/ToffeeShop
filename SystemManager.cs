using System.IO;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.UsersClasses;
using System.Text.RegularExpressions;

namespace CS251_A3_ToffeeShop {
    public class SystemManager {
        private Catalogue catalogue = new Catalogue();
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private User? currentUser;
        private int userInput;

        public void SystemRun() {
            // Load Data at the start of the program
            catalogue.LoadCatalogueData("./Items/Data.json");


            // Login/Register
            Console.WriteLine("\nWelcome to Toffee Shop!\n");

            do {
                Console.WriteLine("\n1) Browse Catalogue\n2) Register\n3) Login\n4) Exit");
                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput) {
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
                        if (currentUser is Admin) {
                            AdminSystem();
                        }

                        // if his type is Staff
                        else if (currentUser is Staff) {
                            StaffSystem();
                        }

                        // if his type is customer
                        if (currentUser is Customer) {
                            CustomerSystem();
                        }
                        break;
                    case 4:
                        Console.WriteLine("Closing Program!\n");
                        return;
                    default:
                        Console.WriteLine("Please choose a valid number!\n");
                        break;
                }
            } while (userInput != 4);

            // Save Data before Closing the program
            catalogue.SaveCatalogueData("./Items/Data.json");
        }

        private void CustomerSystem() {
            if (currentUser == null) return;

            do {
                Console.WriteLine("\n1) Browser Catalogue\n2) Add Item to Shopping Cart\n3) Log out");
                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput) {
                    case 1:     // Browser Catalogue
                        catalogue.DisplayCatalogue();
                        break;
                    case 2:     // Adding Item To Shopping Cart
                        catalogue.DisplayCatalogue();
                        int choice, quantity;

                        Console.Write("Pick an Item to Add: ");
                        int.TryParse(Console.ReadLine(), out choice);
                        Console.Write("How many do you want to Add: ");
                        int.TryParse(Console.ReadLine(), out quantity);

                        if (choice <= 0 || choice >= catalogue.GetProductList().Count) {
                            Console.WriteLine("Invalid choice!");
                            break;
                        }

                        if (quantity <= 0) {
                            Console.WriteLine("Invalid quantity!");
                            break;
                        }

                        ((Customer)currentUser).GetShoppingCart().AddItem(catalogue.GetProductList()[choice - 1], quantity);
                        break;
                    case 3:     // Logging out
                        Console.WriteLine("Logged out Successfully!");
                        break;
                    default:    // Invalid Input
                        Console.WriteLine("Please choose a valid number!\n");
                        break;
                }
            } while (userInput != 3);
        }

        private void StaffSystem() {

        }

        private void AdminSystem() {

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
            do {
                found = false;
                correctInput = false;
                Console.Write("Please Enter your Username (Enter 0 to cancel Registration): ");
                username = Console.ReadLine();
                if (username == "0") {
                    Console.WriteLine("Registration Canceled!");
                    return;
                }

                if (string.IsNullOrEmpty(username) || !usernamePattern.IsMatch(username)) {
                    Console.WriteLine("Invalid Username!\nUsername should consist of letters, numbers, _, -");
                } else {
                    correctInput = true;
                    if (users.ContainsKey(username)) {
                        Console.WriteLine("This username is already taken!");
                        found = true;
                    }
                }
            } while (found || !correctInput || string.IsNullOrEmpty(username));

            // Take Password
            do {
                correctInput = false;
                Console.Write("Please Enter your password (Enter 0 to cancel Registration): ");
                password = Console.ReadLine();
                if (password == "0") {
                    Console.WriteLine("Registration Canceled!");
                    return;
                }

                if (string.IsNullOrEmpty(password) || !passwordPattern.IsMatch(password)) {
                    Console.WriteLine("Invalid password!");
                    Console.WriteLine("Password must consist of letters, numbers and one of [$ # & * % ^] and be at least 8 characters long");
                } else {
                    correctInput = true;
                }
            } while (!correctInput || string.IsNullOrEmpty(password));

            // Take Email
            do {
                correctInput = false;
                Console.Write("Please Enter your Email Address (Enter 0 to cancel Registration): ");
                emailAdress = Console.ReadLine();
                if (emailAdress == "0") {
                    Console.WriteLine("Registration Canceled!");
                    return;
                }

                if (string.IsNullOrEmpty(emailAdress) || !emailPattern.IsMatch(emailAdress)) {
                    Console.WriteLine("Invalid Email Address!");
                    Console.WriteLine("Please enter a valid Email Address!");
                } else {
                    correctInput = true;
                }
            } while (!correctInput || string.IsNullOrEmpty(emailAdress));

            if (!TakeRegistrationInputNoRegex(ref city, "City")) return;

            if (!TakeRegistrationInputNoRegex(ref street, "Street")) return;

            if (!TakeRegistrationInputNoRegex(ref buildingNo, "Building Number")) return;
            Address newaddress = new Address(street, city, buildingNo);
            User customer = new Customer(name, username, password, emailAdress,newaddress);
            users.Add(username, customer);
            Console.WriteLine("Registered Successfully!");
        }

        private bool LoginUser() {
            string? username;
            string? password;
            bool correctInput;

            do {
                correctInput = false;
                Console.WriteLine("Please enter your Username (Enter 0 to cancel Login): ");
                username = Console.ReadLine();
                Console.WriteLine("Please enter your Password (Enter 0 to cancel Login): ");
                password = Console.ReadLine();

                if (username == "0" || password == "0") {
                    Console.WriteLine("Login Canceled!");
                    return false;
                }

                if (string.IsNullOrEmpty(username)) {
                    Console.WriteLine("Username cannot be empty!");
                    continue;
                }

                if (string.IsNullOrEmpty(password)) {
                    Console.WriteLine("Password cannot be empty!");
                    continue;
                }

                if (!users.ContainsKey(username)) {
                    Console.WriteLine("User does not exist!");
                } else {
                    if (users[username].GetPassword() != password) {
                        Console.WriteLine("Incorrect Password!");
                    } else {
                        correctInput = true;
                    }
                }
            } while (!correctInput);

            if (string.IsNullOrEmpty(username)) {
                Console.WriteLine("Login Failed!");
                return false;
            }

            currentUser = users[username];
            Console.WriteLine("Logged in Successfully!");
            Console.WriteLine($"Welcome {currentUser.GetName()}!");
            return true;
        }

        private bool TakeRegistrationInputNoRegex(ref string variable, string strname) {
            bool correctInput;
            string? input;
            do {
                correctInput = false;
                Console.Write($"Please Enter your {strname} (Enter 0 to cancel Registration): ");
                input = Console.ReadLine();
                if (input == "0") {
                    Console.WriteLine("Registration Canceled!");
                    return false;
                }

                if (string.IsNullOrEmpty(input)) {
                    Console.WriteLine($"Invalid {strname}!");
                    Console.WriteLine($"{strname} Cannot be Empty!");
                } else {
                    correctInput = true;
                }
            } while (!correctInput || string.IsNullOrEmpty(input));

            variable = input;

            return true;
        }
    }
}