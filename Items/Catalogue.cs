using System.Text.Json;
namespace CS251_A3_ToffeeShop.Items {
    public class Catalogue {
        private List<Product> productList = new List<Product>();

        public List<Product> GetProductList() {
            return productList;
        }

        public void AddProduct(Product product) {
            productList.Add(product);
        }

        public void RemoveProduct(Product product) {
            productList.Remove(product);
        }

        public void LoadCatalogueData(string file) {
            // Return if can't Open file
            if (!file.EndsWith(".json")) {
                Console.WriteLine("Failed To Open File!");
                return;
            }

            // Open in Read mode
            using (StreamReader reader = new StreamReader(file)) {
                // Read All File
                string json = reader.ReadToEnd();

                // Put all data in ProductData Struct
                List<ProductData>? items = JsonSerializer.Deserialize<List<ProductData>>(json);

                // Return If no Items in file
                if (items == null || items.Count <= 0) return;

                // Loop on each struct and convert it to Product class
                // To insert in Catalogue.productList
                foreach (ProductData productData in items) {
                    Product product = new Product(productData.name, productData.category, productData.price);
                    if (!string.IsNullOrEmpty(productData.description)) product.SetDescription(productData.description);
                    if (!string.IsNullOrEmpty(productData.brand)) product.SetBrand(productData.brand);
                    if (productData.discountPrice != 0) product.SetDiscountPrice(productData.discountPrice);

                    AddProduct(product);
                }
            }
        }

        public void SaveCatalogueData(string file) {
            // Return if can't Open file
            if (!file.EndsWith(".json")) {
                Console.WriteLine("Failed To Open File!");
                return;
            }

            // ProductData Struct to store all the products in
            List<ProductData> dataList = new List<ProductData>();

            // Open File in Write Mode
            using (StreamWriter writer = new StreamWriter(file)) {
                // Loop on each product and add it to the structlist
                foreach(Product product in productList) {
                    ProductData data = new ProductData();
                    data.name = product.GetName();
                    data.category = product.GetCategory();
                    data.description = product.GetDescription();
                    data.brand = product.GetBrand();
                    data.price = product.GetPrice();
                    data.discountPrice = product.GetDiscountPrice();

                    dataList.Add(data);
                }

                // Serialize the struct list to convert it to json
                string json = JsonSerializer.Serialize(dataList.ToArray());
                
                // write the serialized Json to the file
                writer.Write(json);
            }
        }

        public void DisplayCatalogue() {
            int i = 1;
            foreach (Product product in GetProductList()) {
                Console.Write($"{i++}) Name: {product.GetName()} - Price: {product.GetDiscountPrice()} L.E.\n");
                Console.Write($"\tCategory: {product.GetCategory()}");

                // Don't display null or empty brand
                if (!string.IsNullOrEmpty(product.GetBrand())) {
                    Console.Write($" - Brand: {product.GetBrand()}");
                }
                Console.WriteLine();

                // Don't display null or empty description
                if (!string.IsNullOrEmpty(product.GetDescription())) {
                    Console.WriteLine($"\tDescription: {product.GetDescription()}");
                }
            }
        }
        
        // public void AddToShoppingCart(ref ShoppingCart shoppingCart, Product product) {

        // }
    }
}