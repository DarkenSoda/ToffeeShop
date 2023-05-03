using System.IO;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.UsersClasses;

namespace CS251_A3_ToffeeShop {
    public class SystemManager {
        private List<User> users = new List<User>();
        Catalogue catalogue = new Catalogue();

        public void SystemRun() {
            // Load Data at the start of the program
            catalogue.LoadCatalogueData("./Items/Data.json");
            
            
            // Login/Register
            // While(true)


            // Save Data before Closing the program
            catalogue.SaveCatalogueData("./Items/Data.json");
        }

        private void CustomerSystem() {

        }

        private void StaffSystem() {

        }

        private void AdminSystem() {

        }

        public void DisplayCatalogue() {
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