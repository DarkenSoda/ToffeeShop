using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.BalanceClasses;

namespace CS251_A3_ToffeeShop.UsersClasses
{
    public class Admin : User
    {

        public Admin(string name, string userName, string password, string emailAdress) : base(name, userName, password, emailAdress) { }

        public void UpdateCatalogue(Catalogue catalogue)
        {
            int userInput;

            do
            {
                catalogue.DisplayCatalogue();
                Console.WriteLine("1) Add Product\n2) Remove Product\n3) Update Product\n4) Cancel");
                int.TryParse(Console.ReadLine(), out userInput);
                while (!int.TryParse(Console.ReadLine(), out userInput))
                {
                    Console.WriteLine("Invalid Input Try Again! ");
                    Console.WriteLine("1) Add Product\n2) Remove Product\n3) Update Product\n4) Cancel");
                    int.TryParse(Console.ReadLine(), out userInput);
                }
                switch (userInput)
                {
                    case 1:
                        string? name;
                        string? category;
                        string? description;
                        string? brand;
                        double price;
                        Console.WriteLine("Enter Product Name: ");
                        name = Console.ReadLine();
                        while (String.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Enter Product Name: ");
                            name = Console.ReadLine();
                        }
                        Console.WriteLine("Enter Product Category: ");
                        category = Console.ReadLine();
                        while (String.IsNullOrEmpty(category))
                        {
                            Console.WriteLine("Enter Product Category: ");
                            category = Console.ReadLine();
                        }
                        Console.WriteLine("Enter Product Description ");
                        description = Console.ReadLine();
                        while (String.IsNullOrEmpty(description))
                        {
                            Console.WriteLine("Enter Product Description ");
                            description = Console.ReadLine();
                        }
                        Console.WriteLine("Enter Product Brand: ");
                        brand = Console.ReadLine();
                        while (String.IsNullOrEmpty(brand))
                        {
                            Console.WriteLine("Enter Product Brand: ");
                            brand = Console.ReadLine();
                        }
                        Console.WriteLine("Enter Product Price: ");
                        while (!double.TryParse(Console.ReadLine(), out price)) { }
                        catalogue.AddProduct(new Product(name, category, price));
                        break;
                    case 2:
                        catalogue.DisplayCatalogue();
                        int choice;
                        System.Console.WriteLine("Pick a Product To Remove: ");
                        while (!int.TryParse(Console.ReadLine(), out choice)) { }
                        catalogue.RemoveProduct(catalogue.GetProductList()[choice - 1]);
                        break;
                    case 3:
                        catalogue.DisplayCatalogue();
                        int _choice;
                        System.Console.WriteLine("Pick a Product To Update: ");
                        while (!int.TryParse(Console.ReadLine(), out _choice)) { }
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
        private void AddVoucher(ref List<Voucher> voucherList, Voucher newVoucher)
        {
            voucherList.Add(newVoucher);
        }
        private void RemoveVoucher(ref List<Voucher> voucher, int index)
        {
            voucher.RemoveAt(index - 1);
        }
        public void UpdateVouchers(List<Voucher> voucher)
        {
            int x;
            Console.WriteLine("1-Add Voucher.");
            Console.WriteLine("2-Remove Voucher.");
            x = Convert.ToInt32(Console.ReadLine());
            switch (x)
            {
                case 1:
                    string? voucherCode;
                    double discountValue;
                    Console.WriteLine("Enter The Voucher Code: ");
                    voucherCode = Console.ReadLine();
                    while (string.IsNullOrEmpty(voucherCode))
                    {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.WriteLine("Enter The Voucher Code: ");
                        voucherCode = Console.ReadLine();
                    }
                    Console.WriteLine("Enter The Discount Value: ");
                    double.TryParse(Console.ReadLine(), out discountValue);
                    Voucher newVoucher = new Voucher(voucherCode, discountValue);
                    AddVoucher(ref voucher, newVoucher);
                    break;
                case 2:
                    for (int i = 0; i < voucher.Count; i++)
                    {
                        Console.WriteLine($"{i}) " + voucher[i].GetType() + ".");
                    }
                    int y;
                    Console.WriteLine("Chose The Number Of Voucher You Want To Delete: ");
                    int.TryParse(Console.ReadLine(), out y);
                    RemoveVoucher(ref voucher, y);
                    break;
            }
        }
        private void UpdateProduct(Product product)
        {
            Console.WriteLine("1) Update Name.\n2) Update Category\n 3) Update Description\n 4) Update Price\n5) Update Brand\n6) Cancel");
            int choice;
            string? name;
            string? category;
            string? description;
            string? brand;
            double price;
            while (!int.TryParse(Console.ReadLine(), out choice)) { }
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter The New Name: ");
                    name = Console.ReadLine();
                    while (String.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Enter Product Name: ");
                        name = Console.ReadLine();
                    }
                    product.SetName(name);
                    break;
                case 2:
                Console.WriteLine("Enter The New Category: ");
                    category = Console.ReadLine();
                    while (String.IsNullOrEmpty(category))
                    {
                        Console.WriteLine("Enter The New Category: ");
                        category = Console.ReadLine();
                    }
                    product.SetCategory(category);
                    break;
                case 3:
                Console.WriteLine("Enter The New description: ");
                    description = Console.ReadLine();
                    while (String.IsNullOrEmpty(description))
                    {
                        Console.WriteLine("Enter The New Description: ");
                        description = Console.ReadLine();
                    }
                    product.SetDescription(description);
                    break;
                case 4:
                    Console.WriteLine("Enter Product Price: ");
                    while (!double.TryParse(Console.ReadLine(), out price)) {}
                    product.SetPrice(price);
                    break;
                case 5:
                    Console.WriteLine("Enter The New Brand: ");
                        brand = Console.ReadLine();
                        while (String.IsNullOrEmpty(brand))
                        {
                            Console.WriteLine("Enter Product Brand: ");
                            brand = Console.ReadLine();
                        }
                    product.SetBrand(brand);
                    break;
                case 6:
                    Console.WriteLine("Process Canceled!");
                    break;
            }

        }

        public void UpdateLoyalityPoint(double value)
        {
            LoyalityPoints.ChangeDiscountValue(value);
        }
        public void CancelOrder(Order order)
        {
            order.SetOrderStatue(OrderState.Canceled);
        }
        public void UpdateOrder(Order order)
        {
            Console.WriteLine("1-Update Order State.");
            Console.WriteLine("2-Update Order Address.");
            int y;
            y = Convert.ToInt32(Console.ReadLine());
            int.TryParse(Console.ReadLine(), out y);
            switch (y)
            {
                case 1:
                    order.UpdateState();
                    break;
                case 2:
                    string? street;
                    string? city;
                    string? buildingNumber;
                    Console.WriteLine("Enter The Street Name: ");
                    street = Console.ReadLine();
                    while (string.IsNullOrEmpty(street))
                    {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.WriteLine("Enter The Street Name: ");
                        street = Console.ReadLine();
                    }
                    Console.WriteLine("Enter The City: ");
                    city = Console.ReadLine();
                    while (string.IsNullOrEmpty(city))
                    {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.WriteLine("Enter The City: ");
                        city = Console.ReadLine();
                    }
                    Console.WriteLine("Enter The Building Number: ");
                    buildingNumber = Console.ReadLine();
                    while (string.IsNullOrEmpty(buildingNumber))
                    {
                        Console.WriteLine("Invalid Input Try Again! ");
                        Console.WriteLine("Enter The Building Number: ");
                        buildingNumber = Console.ReadLine();
                    }
                    Address newaddress = new Address(street, city, buildingNumber);
                    order.SetAddress(newaddress);
                    break;
            }
        }
        public void SuspendCustomer(Customer customer)
        {
            customer.SetCustomerState(CustomerState.inactive);
        }
    }

    public struct AdminData
    {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}