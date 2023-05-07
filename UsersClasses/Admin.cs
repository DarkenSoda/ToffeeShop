using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.BalanceClasses;

namespace CS251_A3_ToffeeShop.UsersClasses {
    public class Admin : User {

        public Admin(string name, string userName, string password, string emailAdress) : base(name, userName, password, emailAdress) { }

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
                        string? description;
                        string? brand;
                        double price;
                        Console.Write("Enter Product Name: (Enter 0 to Cancel): ");//////////////////
                        name = Console.ReadLine();
                        while (String.IsNullOrEmpty(name)) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Enter Product Name: (Enter 0 to Cancel): ");
                            name = Console.ReadLine();
                        }
                        if (name == "0") { break; }
                        Console.Write("Enter Product Category: (Enter 0 to Cancel): ");
                        category = Console.ReadLine();
                        while (String.IsNullOrEmpty(category)) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Enter Product Category: (Enter 0 to Cancel): ");
                            category = Console.ReadLine();
                        }
                        if (category == "0") { break; }
                        Console.Write("Enter Product Description (Enter 0 to Cancel): ");
                        description = Console.ReadLine();
                        while (String.IsNullOrEmpty(description)) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Enter Product Description (Enter 0 to Cancel): ");
                            description = Console.ReadLine();
                        }
                        if (description == "0") { break; }
                        Console.Write("Enter Product Brand (Enter 0 to Cancel): ");
                        brand = Console.ReadLine();
                        while (String.IsNullOrEmpty(brand)) {
                            Console.WriteLine("Invalid Input Try Again!");
                            Console.Write("Enter Product Brand (Enter 0 to Cancel): ");
                            brand = Console.ReadLine();
                        }
                        if (brand == "0") { break; }
                        Console.Write("Enter Product Price: ");
                        while (!double.TryParse(Console.ReadLine(), out price)) { Console.WriteLine("Invalid Input Try Again!");}
                        catalogue.AddProduct(new Product(name, category, price));
                        break;
                    case 2:
                        catalogue.DisplayCatalogue();
                        int choice;
                        System.Console.Write("Pick a Product To Remove: ");
                        while (!int.TryParse(Console.ReadLine(), out choice)) { Console.WriteLine("Invalid Input Try Again!");}
                        catalogue.RemoveProduct(catalogue.GetProductList()[choice - 1]);
                        break;
                    case 3:
                        catalogue.DisplayCatalogue();
                        int _choice;
                        System.Console.Write("Pick a Product To Update: ");
                        while (!int.TryParse(Console.ReadLine(), out _choice)) { Console.WriteLine("Invalid Input Try Again!");}
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
        private void AddVoucher(ref List<Voucher> voucherList, Voucher newVoucher) {
            voucherList.Add(newVoucher);
        }
        private void RemoveVoucher(ref List<Voucher> voucher, int index) {
            voucher.RemoveAt(index - 1);
        }
        public void UpdateVouchers(List<Voucher> voucher) {
            // Add Cancel
            int userInput;
            Console.WriteLine("1) Add Voucher");
            Console.WriteLine("2) Remove Voucher");
            Console.WriteLine("3) Go Back!");
            userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput) {
                case 1:
                    string? voucherCode;
                    double discountValue;
                    Console.Write("Enter The Voucher Code: ");
                    voucherCode = Console.ReadLine();
                    while (string.IsNullOrEmpty(voucherCode)) {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.Write("Enter The Voucher Code: ");
                        voucherCode = Console.ReadLine();
                    }
                    Console.Write("Enter The Discount Value: ");
                    double.TryParse(Console.ReadLine(), out discountValue);
                    Voucher newVoucher = new Voucher(voucherCode, discountValue);
                    AddVoucher(ref voucher, newVoucher);
                    break;
                case 2:
                    for (int i = 0; i < voucher.Count; i++) {
                        Console.WriteLine($"{i}) " + voucher[i].GetType() + ".");
                    }
                    int choice;
                    // Check input
                    Console.Write("Chose The Number Of Voucher You Want To Delete: ");
                    int.TryParse(Console.ReadLine(), out choice);
                    RemoveVoucher(ref voucher, choice);
                    break;
            }
        }
        private void UpdateProduct(Product product) {
            // Add update discount price
            Console.WriteLine("1) Update Name.\n2) Update Category\n3) Update Description\n4) Update Original Price\n5) Update Brand\n6) Cancel");
            int choice;
            string? name;
            string? category;
            string? description;
            string? brand;
            double price;
            while (!int.TryParse(Console.ReadLine(), out choice)) { }
            // Add Cancel
            switch (choice) {
                case 1:
                    Console.Write("Enter The New Name: ");
                    name = Console.ReadLine();
                    while (String.IsNullOrEmpty(name)) {
                        Console.Write("Enter Product Name: ");
                        name = Console.ReadLine();
                    }
                    product.SetName(name);
                    break;
                case 2:
                    Console.Write("Enter The New Category: ");
                    category = Console.ReadLine();
                    while (String.IsNullOrEmpty(category)) {
                        Console.Write("Enter The New Category: ");
                        category = Console.ReadLine();
                    }
                    product.SetCategory(category);
                    break;
                case 3:
                    Console.Write("Enter The New description: ");
                    description = Console.ReadLine();
                    while (String.IsNullOrEmpty(description)) {
                        Console.Write("Enter The New Description: ");
                        description = Console.ReadLine();
                    }
                    product.SetDescription(description);
                    break;
                case 4:
                    Console.Write("Enter Product Price: ");
                    while (!double.TryParse(Console.ReadLine(), out price)) { }
                    product.SetPrice(price);
                    break;
                case 5:
                    Console.Write("Enter The New Brand: ");
                    brand = Console.ReadLine();
                    while (String.IsNullOrEmpty(brand)) {
                        Console.Write("Enter Product Brand: ");
                        brand = Console.ReadLine();
                    }
                    product.SetBrand(brand);
                    break;
                case 6:
                    Console.WriteLine("Process Canceled!");
                    break;
            }

        }

        public void UpdateLoyalityPoint(double value) {
            LoyalityPoints.ChangeDiscountValue(value);
        }
        public void CancelOrder(Order order) {
            order.SetOrderStatue(OrderState.Canceled);
        }
        public void UpdateOrder(Order order) {
            // Add Cancel
            Console.WriteLine("1) Update Order State");
            Console.WriteLine("2) Update Order Address");
            Console.WriteLine("3) Go Back!");
            int userInput;
            userInput = Convert.ToInt32(Console.ReadLine());
            int.TryParse(Console.ReadLine(), out userInput);
            switch (userInput) {
                case 1:
                    order.UpdateState();
                    break;
                case 2:
                    string? street;
                    string? city;
                    string? buildingNumber;
                    Console.Write("Enter The Street Name: ");
                    street = Console.ReadLine();
                    while (string.IsNullOrEmpty(street)) {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.Write("Enter The Street Name: ");
                        street = Console.ReadLine();
                    }
                    Console.Write("Enter The City: ");
                    city = Console.ReadLine();
                    while (string.IsNullOrEmpty(city)) {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.Write("Enter The City: ");
                        city = Console.ReadLine();
                    }
                    Console.Write("Enter The Building Number: ");
                    buildingNumber = Console.ReadLine();
                    while (string.IsNullOrEmpty(buildingNumber)) {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.Write("Enter The Building Number: ");
                        buildingNumber = Console.ReadLine();
                    }
                    Address newaddress = new Address(street, city, buildingNumber);
                    order.SetAddress(newaddress);
                    break;
            }
        }
        public void SuspendCustomer(Customer customer) {
            customer.SetCustomerState(CustomerState.inactive);
        }
    }

    public struct AdminData {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}