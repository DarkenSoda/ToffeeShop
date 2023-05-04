using System.IO;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.UsersClasses;

namespace CS251_A3_ToffeeShop {
    public class SystemManager {
        private List<User> users = new List<User>();
        private Catalogue catalogue = new Catalogue();

        public void SystemRun() {
            // Load Data at the start of the program
            catalogue.LoadCatalogueData("./Items/Data.json");


            // Login/Register
            // While(true)
            catalogue.DisplayCatalogue();


            // Save Data before Closing the program
            catalogue.SaveCatalogueData("./Items/Data.json");
        }

        private void CustomerSystem() {

        }

        private void StaffSystem() {

        }

        private void AdminSystem() {

        }
    }
}