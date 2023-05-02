using CS251_A3_ToffeeShop.Items;

namespace CS251_A3_ToffeeShop {
    public class SystemManager {
        // private List<User> users = new List<User>();
        Catalogue catalogue = new Catalogue();

        public void SystemRun() {

        }

        private void CustomerSystem() {

        }

        private void StaffSystem() {

        }

        private void AdminSystem() {

        }

        private void DisplayCatalogue() {
            int i = 1;
            foreach (Product product in catalogue.GetProductList()) {
                Console.Write($"{i++}) Name: {product.GetName()} - Price: {product.GetDiscountPrice()} L.E.\n");
                Console.Write($"\tCategory: {product.GetCategory()}");
                if (product.GetBrand() != "") {
                    Console.Write($" - Brand: {product.GetBrand()}");
                }
                Console.WriteLine();
                if (product.GetDescription() != "") {
                    Console.WriteLine($"\tDescription: {product.GetDescription()}");
                }
            }
        }
    }
}