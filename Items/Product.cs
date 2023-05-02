namespace CS251_A3_ToffeeShop.Items {
    public class Product {
        private string name;
        private string category;
        private string? description;
        private string? brand;
        private float price;
        private float discountPrice;

        public Product(string name, string category, float price = 0) {
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

        public float GetPrice() {
            return price;
        }

        public void SetPrice(float price) {
            this.price = price;
        }

        public float GetDiscountPrice() {
            return discountPrice;
        }

        public void SetDiscountPrice(float price) {
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
}