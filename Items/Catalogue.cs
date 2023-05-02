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

        // public void AddToShoppingCart(ref ShoppingCart shoppingCart, Product product) {

        // }
    }
}