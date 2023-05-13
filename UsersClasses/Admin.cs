using System;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.BalanceClasses;

/* The above code defines a C# class called "Admin" which inherits from a class called "User". The
Admin class has methods to update the product catalogue, update vouchers, update loyalty points,
cancel orders, update orders, and suspend customers. It also defines a struct called "AdminData"
which contains information about an admin user. */
namespace CS251_A3_ToffeeShop.UsersClasses {
    public class Admin : User {
        /* The above code is defining a constructor for a class called "Admin" in C#. The constructor
        takes four parameters: "name", "userName", "password", and "emailAddress", and calls the
        constructor of the base class (presumably a class called "User") with those parameters using
        the "base" keyword. This allows the "Admin" class to inherit the properties and methods of
        the "User" class. */
        public Admin(string name, string userName, string password, string emailAdress) : base(name, userName, password, emailAdress) { }

        /// This function updates a catalogue by allowing the Admin to add, remove, or update products in
        /// the catalogue.
        /// 
        /// @param Catalogue A class representing a collection of products.
        public void UpdateCatalogue(Catalogue catalogue) {
            int userInput;

            do {
                catalogue.DisplayCatalogue();
                Console.WriteLine("1) Add Product\n2) Remove Product\n3) Update Product\n4) Cancel");
                while (!int.TryParse(Console.ReadLine(), out userInput)) {
                    Console.WriteLine("Invalid Input Try Again! ");
                    Console.WriteLine("1) Add Product\n2) Remove Product\n3) Update Product\n4) Cancel");
                    int.TryParse(Console.ReadLine(), out userInput);
                }
                switch (userInput) {
                    case 1:
                        string? name;
                        string? category;
                        double price;
                        Console.Write("Enter Product Name (Enter 0 to Cancel): ");
                        name = Console.ReadLine();
                        while (String.IsNullOrEmpty(name)) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Enter Product Name (Enter 0 to Cancel): ");
                            name = Console.ReadLine();
                        }
                        if (name == "0") { break; }
                        Console.Write("Enter Product Category (Enter 0 to Cancel): ");
                        category = Console.ReadLine();
                        while (String.IsNullOrEmpty(category)) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Enter Product Category (Enter 0 to Cancel): ");
                            category = Console.ReadLine();
                        }
                        if (category == "0") { break; }
                        Console.Write("Enter Product Price (Enter 0 to Cancel): ");
                        while (!double.TryParse(Console.ReadLine(), out price) || price < 0) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Enter Product Price (Enter 0 to Cancel): ");
                        }
                        if (price == 0) { break; }
                        Product newProduct = new Product(name, category, price);
                        catalogue.AddProduct(newProduct);
                        break;
                    case 2:
                        catalogue.DisplayCatalogue();
                        int choice;
                        Console.Write("Pick a Product To Remove: ");
                        while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice > catalogue.GetProductList().Count) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Pick a Product To Remove: ");
                        }
                        catalogue.RemoveProduct(catalogue.GetProductList()[choice - 1]);
                        break;
                    case 3:
                        catalogue.DisplayCatalogue();
                        int _choice;
                        Console.Write("Pick a Product To Update: ");
                        while (!int.TryParse(Console.ReadLine(), out _choice) || _choice <= 0 || _choice > catalogue.GetProductList().Count) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Pick a Product To Update: ");
                        }
                        UpdateProduct(catalogue.GetProductList()[_choice - 1]);
                        break;
                    case 4:
                        Console.WriteLine("Process Canceled!");
                        break;
                    default:    // Invalid Input
                        Console.WriteLine("Please choose a valid number!\n");
                        break;
                }
            } while (userInput != 4);
        }

        /// The function adds a new voucher to a list of vouchers, converting the value to a percentage.
        /// 
        /// @param voucherList A reference to a List of double values that represents a collection of
        /// vouchers.
        /// @param newVoucher A double value representing the new voucher to be added to the
        /// voucherList. It is divided by 100 before being added to the list.
        private void AddVoucher(ref List<double> voucherList, double newVoucher) {
            voucherList.Add(newVoucher / 100);
        }

        /// This function removes an element from a list of doubles at a specified index.
        /// 
        /// @param voucher A reference to a List of double values that represents a collection of
        /// vouchers.
        /// @param index The index parameter is an integer that represents the position of the element
        /// to be removed from the voucher list. The method removes the element at the specified index
        /// minus one, as list indices start at zero.
        private void RemoveVoucher(ref List<double> voucher, int index) {
            voucher.RemoveAt(index - 1);
        }

        /// This function updates a list of vouchers by allowing the Admin to add, remove, or update the
        /// maximum amount spent for a voucher.
        /// 
        /// @param voucher A list of double values representing the discount percentage of vouchers.
        public void UpdateVouchers(List<double> voucher) {
            // Add Cancel
            int userInput;
            do {
                Console.WriteLine("1) Add Voucher");
                Console.WriteLine("2) Remove Voucher");
                Console.WriteLine("3) Update Maximum Amount Spent!");
                Console.WriteLine("4) Go Back!");
                userInput = Convert.ToInt32(Console.ReadLine());
                switch (userInput) {
                    case 1:
                        double discountValue;
                        Console.Write("Enter The Discount Percentage: (if you want to cancel enter '0') ");
                        while (!double.TryParse(Console.ReadLine(), out discountValue) || discountValue < 0 || discountValue > 100) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Enter The Discount Percentage: (if you want to cancel enter '0') ");
                        }
                        if (discountValue == 0) { break; }
                        Voucher.SetVoucherNumber(Voucher.GetVoucherNumber() + 1);
                        AddVoucher(ref voucher, discountValue);
                        break;
                    case 2:
                        if (voucher.Count == 0) {
                            Console.WriteLine("Voucher List Is Empty!");
                            break;
                        }
                        for (int i = 0; i < voucher.Count; i++) {
                            Console.WriteLine($"{i + 1}) Voucher: " + voucher[i] * 100 + "%");
                        }
                        int choice;
                        // Check input
                        Console.Write("Chose The Number Of Voucher You Want To Delete: (if you want to cancel enter '0') ");
                        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > voucher.Count) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Chose The Number Of Voucher You Want To Delete: (if you want to cancel enter '0') ");
                        }
                        if (choice == 0) { break; }
                        RemoveVoucher(ref voucher, choice);
                        Console.WriteLine("Voucher Removed Successfully!");
                        break;
                    case 3:
                        Console.Write("enter the new amount to be added[1 ~ 100]:");
                        while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice > 100) {
                            Console.WriteLine("Enter a Valid Input!");
                        }
                        Customer.SetMaxMoneySpentForVoucher(choice);
                        break;
                    case 4:
                        Console.WriteLine("Process canceled.");
                        break;
                }
            } while (userInput != 4);
        }

        /// The function allows for updating various properties of a product, including its name,
        /// category, description, brand, price, and discount price.
        /// 
        /// @param Product The product object that needs to be updated. It contains information such as
        /// name, category, description, brand, price, and discount price.
        private void UpdateProduct(Product product) {
            // Add update discount price
            int choice;
            do {
                Console.WriteLine($"Name: {product.GetName()}");
                Console.WriteLine($"\tPrice: {product.GetPrice()} L.E. - Discount Price: {product.GetDiscountPrice()} L.E.");
                Console.Write($"\tCategory: {product.GetCategory()}");

                // Don't display null or empty brand
                if (!string.IsNullOrEmpty(product.GetBrand())) {
                    Console.Write($" - Brand: {product.GetBrand()}");
                }
                Console.WriteLine();

                // Don't display null or empty description
                if (!string.IsNullOrEmpty(product.GetDescription())) {
                    Console.WriteLine($"\tDescription: {product.GetDescription()}");
                }
                Console.WriteLine("1) Update Name\n2) Update Category\n3) Update Description\n4) Update Brand\n5) Update Original Price\n6) Update Discount Price\n7) Cancel");
                string? name;
                string? category;
                string? description;
                string? brand;
                double price;
                double discountPrice;
                while (!int.TryParse(Console.ReadLine(), out choice)) {
                    Console.WriteLine("Invalid input!");
                }
                // Add Cancel
                switch (choice) {
                    case 1:
                        Console.Write("Enter The New Name: (if you want to cancel enter '0') ");
                        name = Console.ReadLine();
                        while (String.IsNullOrEmpty(name)) {
                            Console.Write("Enter Product Name: (if you want to cancel enter '0') ");
                            name = Console.ReadLine();
                        }
                        if (name == "0") { break; }
                        product.SetName(name);
                        Console.WriteLine("Product Name Updated Succesfully!");
                        break;
                    case 2:
                        Console.Write("Enter The New Category: (if you want to cancel enter '0') ");
                        category = Console.ReadLine();
                        while (String.IsNullOrEmpty(category)) {
                            Console.Write("Enter The New Category: (if you want to cancel enter '0') ");
                            category = Console.ReadLine();
                        }
                        if (category == "0") { break; };
                        product.SetCategory(category);
                        Console.WriteLine("Product Category Updated Succesfully!");
                        break;
                    case 3:
                        Console.Write("Enter The New description: (if you want to cancel enter '0') ");
                        description = Console.ReadLine();
                        while (String.IsNullOrEmpty(description)) {
                            Console.Write("Enter The New Description: (if you want to cancel enter '0') ");
                            description = Console.ReadLine();
                        }
                        if (description == "0") { break; };
                        product.SetDescription(description);
                        Console.WriteLine("Product Description Updated Succesfully!");
                        break;
                    case 4:
                        Console.Write("Enter The New Brand: (if you want to cancel enter '0') ");
                        brand = Console.ReadLine();
                        while (String.IsNullOrEmpty(brand)) {
                            Console.Write("Enter Product Brand: (if you want to cancel enter '0') ");
                            brand = Console.ReadLine();
                        }
                        if (brand == "0") { break; }
                        product.SetBrand(brand);
                        Console.WriteLine("Product Brand Updated Succesfully!");
                        break;
                    case 5:
                        Console.Write("Enter Product Price: ");
                        while (!double.TryParse(Console.ReadLine(), out price) || price <= 0) {
                            Console.WriteLine("Invalid input!");
                        }
                        product.SetPrice(price);
                        Console.WriteLine("Product Price Updated Succesfully!");
                        break;

                    case 6:
                        Console.Write("Enter Product Discount Price: ");
                        while (!double.TryParse(Console.ReadLine(), out discountPrice) || discountPrice <= 0) {
                            Console.WriteLine("Invalid input!");
                        }
                        product.SetDiscountPrice(discountPrice);
                        Console.WriteLine("Product Discount Price Updated Succesfully!");
                        break;
                    case 7:
                        Console.WriteLine("Process Canceled!");
                        break;
                }
            } while (choice != 7);
        }

        /// This function updates the loyalty point discount value.
        /// 
        /// @param value The value parameter is a double type input that represents the new discount
        /// value to be set for the LoyaltyPoints object.
        public void UpdateLoyalityPoint(double value) {
            LoyalityPoints.ChangeDiscountValue(value);
        }

        /// The function cancels an order by setting its status to "Canceled".
        /// 
        /// @param Order Order is an object of a class that represents an order in a system. It may
        /// contain information such as the customer's name, the items ordered, the order date, and the
        /// order status. The CancelOrder method takes an Order object as a parameter to cancel the
        /// order by setting its status to "
        public void CancelOrder(Order order) {
            order.SetOrderStatue(OrderState.Canceled);
        }

        /// This function allows the Admin to update the state or address of an order.
        /// 
        /// @param Order The "Order" parameter is an object of the "Order" class, which contains
        /// information about a customer's order, such as the items ordered, the order status, and the
        /// delivery address. The method "UpdateOrder" takes this object as input and allows the Admin to
        /// update the order's state
        public void UpdateOrder(Order order) {
            // Add Cancel
            int userInput;
            do {
                Console.WriteLine("1) Update Order State");
                Console.WriteLine("2) Update Order Address");
                Console.WriteLine("3) Go Back!");

                int.TryParse(Console.ReadLine(), out userInput);
                switch (userInput) {
                    case 1:
                        order.UpdateState();
                        break;
                    case 2:
                        string? street;
                        string? city;
                        string? buildingNumber;
                        Console.Write("Enter The Street Name (if you want to cancel enter '0'): ");
                        street = Console.ReadLine();
                        while (string.IsNullOrEmpty(street)) {
                            Console.WriteLine("Invalid Input Try Again! ");
                            Console.Write("Enter The Street Name (if you want to cancel enter '0'): ");
                            street = Console.ReadLine();
                        }
                        if (name == "0") { break; }
                        Console.Write("Enter The City (if you want to cancel enter '0'): ");
                        city = Console.ReadLine();
                        while (string.IsNullOrEmpty(city)) {
                            Console.WriteLine("Invalid Input Try Again! ");
                            Console.Write("Enter The City (if you want to cancel enter '0'): ");
                            city = Console.ReadLine();
                        }
                        if (city == "0") { break; }
                        Console.Write("Enter The Building Number (if you want to cancel enter '0'): ");
                        buildingNumber = Console.ReadLine();
                        while (string.IsNullOrEmpty(buildingNumber)) {
                            Console.WriteLine("Invalid Input Try Again! ");
                            Console.Write("Enter The Building Number (if you want to cancel enter '0'): ");
                            buildingNumber = Console.ReadLine();
                        }
                        if (buildingNumber == "0") { break; }
                        Address newaddress = new Address(street, city, buildingNumber);
                        order.SetAddress(newaddress);
                        break;
                    case 3:
                        Console.WriteLine("Process Canceled!");
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            } while (userInput != 3);
        }

        /// The function suspends or reactivates a customer based on their current state.
        /// 
        /// @param Customer The "Customer" parameter is an object of the "Customer" class, which
        /// represents a customer in a system. It likely contains information such as the customer's
        /// name, contact information, and account status.
        public void SuspendCustomer(Customer customer) {
            if (customer.GetCustomerState() == CustomerState.active) {
                customer.SetCustomerState(CustomerState.inactive);
            } else {
                customer.SetCustomerState(CustomerState.active);
            }
        }
    }

    /* The above code is defining a C# struct called "AdminData" which has properties for name,
    username, password, phone, email, and isAuthenticated. These properties are all strings except
    for isAuthenticated which is a boolean. The struct is used to store data related to an admin
    user in the database. */
    public struct AdminData {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool isAuthenticated { get; set; }
    }
}