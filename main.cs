using CS251_A3_ToffeeShop.CartClasses;

using CS251_A3_ToffeeShop.BalanceClasses;

namespace CS251_A3_ToffeeShop {
    public class main {
        public static void Main(string[] args) {
            ShoppingCart sc = new ShoppingCart();
            sc.AddItem(new Items.Product("Candy1","Candies",2.50));
            sc.AddItem(new Items.Product("Candy2","Candies",2.50));
            sc.AddItem(new Items.Product("Candy3","Candies",2.50));
            Console.WriteLine(sc.CalculateTotalPrice());
            LoyalityPoints points = new LoyalityPoints();
            points.AddPoints(200);
            sc.ApplyLoyalityPoints(points,2);
            Console.WriteLine(sc.CalculateTotalPrice());
            Console.WriteLine(points.GetPoints());
        }
    }
}