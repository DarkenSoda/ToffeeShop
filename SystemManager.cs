using System.IO;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.UsersClasses;
using System.Text.RegularExpressions;

namespace CS251_A3_ToffeeShop {
    public class SystemManager {
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private Catalogue catalogue = new Catalogue();
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

                        // Check if user is found in DB

                        // if his type is Admin
                        AdminSystem();

                        // if his type is Staff
                        StaffSystem();

                        // if his type is customer
                        CustomerSystem();
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
            do {
                Console.WriteLine("\n1) Browser Catalogue\n2) idk\n3) Log out");
                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput) {
                    case 1:
                        // Browser Catalogue
                        catalogue.DisplayCatalogue();
                        break;
                    case 2:
                        break;
                    case 3:
                        // Logged out
                        Console.WriteLine("Logged out Successfully!");
                        break;
                    default:
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
            string? name;
            string? username;
            string? password;
            string? emailAdress;
            Regex usernamePattern = new Regex("^[a-zA-Z0-9_-]+$");
            Regex passwordPattern = new Regex("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[$#&*%^])[A-Za-z\\d$#&*%^]{8,}$");
            Regex emailPattern = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");

            bool correctInput;

            do {
                correctInput = false;
                Console.Write("Please Enter your Name (Enter 0 to cancel Registration): ");
                name = Console.ReadLine();
                if (name == "0") {
                    Console.WriteLine("Registration Canceled!");
                    return;
                }

                if (name == null) {
                    Console.WriteLine("Invalid name!\nName should not be empty!");
                }
                else {
                    correctInput = true;
                }
            } while (!correctInput || name == null);

            bool found;
            do {
                found = false;
                correctInput = false;
                Console.Write("Please Enter your Username (Enter 0 to cancel Registration): ");
                username = Console.ReadLine();
                if(username == "0") {
                    Console.WriteLine("Registration Canceled!");
                    return;
                }

                if(username == null || !usernamePattern.IsMatch(username)) {
                    Console.WriteLine("Invalid Username!\nUsername should consist of letters, numbers, _, -");
                }
                else{
                    correctInput = true;
                    if (users.ContainsKey(username)) {
                        Console.WriteLine("This username is already taken!");
                        found = true;
                    }
                }
            } while (found || !correctInput || username == null);

            do {
                correctInput = false;
                Console.Write("Please Enter your password (Enter 0 to cancel Registration): ");
                password = Console.ReadLine();
                if (password == "0") {
                    Console.WriteLine("Registration Canceled!");
                    return;
                }

                if (password == null || !passwordPattern.IsMatch(password)) {
                    Console.WriteLine("Invalid password!");
                    Console.WriteLine("Password must consist of letters, numbers and one of [$ # & * % ^] and be at least 8 characters long");
                } else {
                    correctInput = true;
                }
            } while (!correctInput || password == null);

            do {
                correctInput = false;
                Console.Write("Please Enter your Email Address (Enter 0 to cancel Registration): ");
                emailAdress = Console.ReadLine();
                if (emailAdress == "0") {
                    Console.WriteLine("Registration Canceled!");
                    return;
                }

                if (emailAdress == null || !emailPattern.IsMatch(emailAdress)) {
                    Console.WriteLine("Invalid Email Address!");
                    Console.WriteLine("Please enter a valid Email Address!");
                } else {
                    correctInput = true;
                }
            } while (!correctInput || emailAdress == null);

            User customer = new User(name, username, password, emailAdress);
            users.Add(username, customer);
            Console.WriteLine("Registered Successfully!");
        }
    }
}