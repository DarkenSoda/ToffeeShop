using CS251_A3_ToffeeShop.CartClasses;
namespace CS251_A3_ToffeeShop.UsersClasses
{
    public class Staff : User
    {
        public Staff(string name, string userName, string password, string emailAdress) : base(name, userName, password, emailAdress) { }
        public void UpdateOrder(Order order)
        {
            Console.WriteLine("1-Update Order State.");
            Console.WriteLine("2-Update Order Address.");
            int y;
            y = Convert.ToInt32(Console.ReadLine());
            if (y < 1 || y > 2)
            {
                UpdateOrder(order);
            }
            else
            {
                if (y == 1)
                {
                    order.UpdateState();
                }
                else if (y == 2)
                {
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
                }
            }


        }
    }
}