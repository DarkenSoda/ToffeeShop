using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.CartClasses;
using CS251_A3_ToffeeShop.BalanceClasses;

namespace CS251_A3_ToffeeShop.UsersClasses
{
    public class Admin : Staff
    {

        public Admin(string name, string userName, string password, string emailAdress) : base(name, userName, password, emailAdress) { }

        public void UpdateCatalogue(Catalogue catalogue)
        {
            int x;
            Console.WriteLine("1-Add Item." + '\n' + "2-Remove Item.");
            x = Convert.ToInt32(Console.ReadLine());

            if (x == 1)
            {
                string? name;
                string? category;
                double price;
                Console.WriteLine("Enter The Product Name:");
                name = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Enter The Product Category:");
                category = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Enter The Product Price:");
                price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine();
                Product newProduct = new Product(name, category, price);
                catalogue.AddProduct(newProduct);
            }
            else if (x == 2)
            {
                int y;
                catalogue.DisplayCatalogue();
                Console.WriteLine("Chose The Number Of Product You Want To Delete:");
                y = Convert.ToInt32(Console.ReadLine());
                catalogue.RemoveProduct(catalogue.GetProductList()[y - 1]);
            }
        }
        private void AddVoucher(ref List<Voucher> voucher, Voucher newVoucher)
        {
            voucher.Add(newVoucher);
        }
        private void RemoveVoucher(ref List<Voucher> voucher, int x)
        {
            voucher.RemoveAt(x - 1);
        }
        public void UpdataVouchers(List<Voucher> voucher)
        {
            int x;
            Console.WriteLine("1-Add Voucher.");
            Console.WriteLine("2-Remove Voucher.");
            x = Convert.ToInt32(Console.ReadLine());
            if (x == 1)
            {
                string? voucherCode;
                double discountValue;
                Console.WriteLine("Enter The Voucher Code: ");
                voucherCode = Console.ReadLine();
                Console.WriteLine("Enter The Discount Value: ");
                discountValue = Convert.ToDouble(Console.ReadLine());
                Voucher newVoucher = new Voucher(voucherCode, discountValue);
                AddVoucher(ref voucher, newVoucher);
            }
            else if (x == 2)
            {
                for (int i = 0; i < voucher.Count; i++)
                {
                    Console.Write(voucher[i] + " , ");
                }
                int y;
                Console.WriteLine("Chose The Number Of Voucher You Want To Delete:");
                y = Convert.ToInt32(Console.ReadLine());
                RemoveVoucher(ref voucher, y);
            }
        }
        public void UpdateLoyalityPoint(LoyalityPoints loyalityPoints)
        {
            loyalityPoints.SetDiscountValue();
        }
        public void CancelOrder(Order order)
        {
            order.SetOrderStatue(OrderState.Canceled);
        }
        public void UpdataOrder(Order order)
        {
            Console.WriteLine("1-Update Order State.");
            Console.WriteLine("2-Update Order Address.");
            int y;
            y = Convert.ToInt32(Console.ReadLine());
            if (y < 1 || y > 2)
            {
                UpdataOrder(order);
            }
            else
            {
                if (y == 1)
                {
                    order.UpdateState();
                }
                else if (y == 2)
                {
                    string street;
                    string city;
                    string buildingNumber;
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
                }
            }
        }
        public void SuspendCustomer(Customer customer)
        {
            customer.SetCustomerState(Customer.CustomerState.inactive);
        }
    }
}