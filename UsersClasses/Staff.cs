using CS251_A3_ToffeeShop.CartClasses;
namespace CS251_A3_ToffeeShop.UsersClasses {
    public class Staff : User{
        public Staff(string name, string userName, string password, string emailAdress):base(name,userName,password,emailAdress){}
        public void UpdataOrder(Order order){
            Console.WriteLine("1-Update Order State.");
            Console.WriteLine("2-Update Order Address.");
            Console.WriteLine("3-Update Order DateTime.");
            int y;
            y = Convert.ToInt32(Console.ReadLine());
            if (y==1)
            {
                order.UpdateState();
            }else if (y==2)
            {
                string street;
                string city;
                string buildingNumber;
                Console.WriteLine("Enter The Street Name: ");
                street = Console.ReadLine();
                Console.WriteLine("Enter The City: ");
                city = Console.ReadLine();
                Console.WriteLine("Enter The Building Number: ");
                buildingNumber = Console.ReadLine();
                Address newaddress = new Address(street,city,buildingNumber);
                order.SetAddress(newaddress);
            }else if (y==3)
            {
                string dateTime;
                Console.WriteLine("Enter The New DateTime: ");
                dateTime = Console.ReadLine();
                order.SetDateTime(dateTime);
            }
        }
    }
}