/* This code defines a namespace `CS251_A3_ToffeeShop.Items` which contains a class `Product` and a
struct `ProductData`. */
namespace CS251_A3_ToffeeShop.Items {
    public class Product {
        private string name;
        private string category;
        private string? description;
        private string? brand;
        private double price;
        private double discountPrice;

        public Product(string name, string category, double price) {
            this.name = name;
            this.category = category;
            this.price = this.discountPrice = price;
        }

        public string GetName() {
            return name;
        }

        public void SetName(string name) {
            this.name = name;
        }

        public double GetPrice() {
            return price;
        }

        public void SetPrice(double price) {
            this.price = price;
        }

        public double GetDiscountPrice() {
            return discountPrice;
        }

        public void SetDiscountPrice(double price) {
            discountPrice = price;
        }

        public string GetCategory() {
            return category;
        }

        public void SetCategory(string category) {
            this.category = category;
        }

        public void SetDescription(string desc) {
            this.description = desc;
        }

        public string GetDescription() {
            if (description == null) return "";

            return description;
        }

        public void SetBrand(string brand) {
            this.brand = brand;
        }

        public string GetBrand() {
            if (brand == null) return "";

            return brand;
        }
    }

    /* The `ProductData` struct is defining a database schema for a product. It contains properties for
    the name, category, description, brand, price, and discount price of a product. This struct can
    be used to load data from a database or other source and then convert it into a `Product`
    object. */
    public struct ProductData {
        public string name { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string brand { get; set; }
        public double price { get; set; }
        public double discountPrice { get; set; }
    }
}