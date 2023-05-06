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
                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput) {
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
            int x;
            Console.WriteLine("1-Add Voucher.");
            Console.WriteLine("2-Remove Voucher.");
            x = Convert.ToInt32(Console.ReadLine());
            if (x == 1) {
                string? voucherCode;
                double discountValue;
                Console.WriteLine("Enter The Voucher Code: ");
                voucherCode = Console.ReadLine();
                Console.WriteLine("Enter The Discount Value: ");
                discountValue = Convert.ToDouble(Console.ReadLine());
                Voucher newVoucher = new Voucher(voucherCode, discountValue);
                AddVoucher(ref voucher, newVoucher);
            } else if (x == 2) {
                for (int i = 0; i < voucher.Count; i++) {
                    Console.WriteLine($"{i}) " + voucher[i].GetType() + " . ");
                }
                int y;
                Console.WriteLine("Chose The Number Of Voucher You Want To Delete:");
                y = Convert.ToInt32(Console.ReadLine());
                RemoveVoucher(ref voucher, y);
            }
        }
        public void UpdateLoyalityPoint(double value) {
            LoyalityPoints.ChangeDiscountValue(value);
        }
        public void CancelOrder(Order order) {
            order.SetOrderStatue(OrderState.Canceled);
        }
        public void UpdateOrder(Order order) {
            Console.WriteLine("1-Update Order State.");
            Console.WriteLine("2-Update Order Address.");
            int y;
            y = Convert.ToInt32(Console.ReadLine());
            if (y < 1 || y > 2) {
                UpdateOrder(order);
            } else {
                if (y == 1) {
                    order.UpdateState();
                } else if (y == 2) {
                    string street;
                    string city;
                    string buildingNumber;
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
                    order.SetAddress(newaddress);
                }
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