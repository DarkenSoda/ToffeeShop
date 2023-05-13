using System;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.BalanceClasses;


/* The above code defines a class called ShoppingCart which represents a shopping cart for a Toffee
Shop. It contains methods for adding, removing, and changing the quantity of items in the cart, as
well as applying vouchers and calculating the total cost. It also has a method for printing the
items in the cart and a method for clearing the cart. The code also includes a struct called
ShoppingCartData which is used to store the data of a shopping cart. */
namespace CS251_A3_ToffeeShop.CartClasses {
    public class ShoppingCart {
        private Dictionary<Product, int> items = new Dictionary<Product, int>();
        private List<Voucher> currentApplied = new List<Voucher>();
        private double totalCost = 0;
        private int currentAppliedPoints = 0;
        public ShoppingCart() { }

        /* This is a constructor for the ShoppingCart class that takes in another ShoppingCart object
        as a parameter. It initializes a new ShoppingCart object with the same total cost and items
        as the ShoppingCart object passed in. It does this by setting the totalCost property of the
        new object to the totalCost property of the passed in object, and then iterating through the
        items dictionary of the passed in object and adding each key-value pair to the items
        dictionary of the new object. */
        public ShoppingCart(ShoppingCart shoppingCart) {
            this.totalCost = shoppingCart.totalCost;
            foreach (var item in shoppingCart.items) {
                this.items.Add(item.Key, item.Value);
            }
        }

        public void PrintItems() {
            int i = 1;
            foreach (var kvp in items) {
                Console.WriteLine(i + ",  {0}   Quantity: {2}      Cost: {1} L.E", kvp.Key.GetName(), kvp.Key.GetDiscountPrice() * kvp.Value, kvp.Value);
                i++;
            }
            Console.WriteLine("--------------[ Total Cost: {0} L.E ]--------------", totalCost);
        }

        /// This function adds a product item with a specified quantity to a dictionary and updates the
        /// total cost accordingly.
        /// 
        /// @param Product The "Product" parameter is an object of the "Product" class, which represents
        /// a product that can be added to a shopping cart or a list of items. It contains information
        /// about the product such as its name, price, and description.
        /// @param quantity The quantity parameter represents the number of items of the Product type
        /// that the method should add to the items dictionary.
        public void AddItem(Product item, int quantity) {
            if (items.ContainsKey(item)) {
                items[item] += quantity;
            } else {
                items.Add(item, quantity);
            }
            totalCost += item.GetDiscountPrice() * quantity;
        }

        /// This function removes a product item from a dictionary if it exists, otherwise it prints a
        /// message indicating that the item is not found.
        /// 
        /// @param Product Product is a class or data type that represents a product. It likely has
        /// properties such as name, price, description, and possibly other attributes that describe the
        /// product. The RemoveItem method takes an instance of the Product class as a parameter, which
        /// is used to identify the item to be removed from a
        public void RemoveItem(Product item) {
            if (!items.ContainsKey(item))
                Console.WriteLine("Item is not Found!");
            else
                items.Remove(item);
        }

        /// This function allows the user to change the quantity of a selected item in a list.
        /// 
        /// @param identifier The identifier is a string parameter that is not used in this method. It
        /// is likely used in other parts of the code to identify a specific item or product.
        /// 
        /// @return If the item list is empty or null, the method returns without making any changes. If
        /// the user cancels the process by entering 0, the method returns without making any changes.
        /// Otherwise, the method calls the ChangeItem method with the specified identifier, user input,
        /// and quantity. There is no explicit return statement in this code snippet.
        public void ChangeQuantity(string identifier) {
            if (items.Count <= 0 || items == null) {
                Console.WriteLine("Item List is empty!");
                return;
            }
            int quantity;
            int userInput;
            do {
                System.Console.Write("Enter Product number (Enter 0 to Cancel): ");
                int.TryParse(Console.ReadLine(), out userInput);

                if (userInput == 0) {
                    Console.WriteLine("Process Canceled!");
                    return;
                }

                Console.Write("Enter a Quantity: ");
                int.TryParse(Console.ReadLine(), out quantity);

                if (userInput < 0 || userInput > items.Count()) {
                    Console.WriteLine("Invalid Choice!\nPlease pick an item from the list!");
                }
            } while (userInput < 0 || userInput > items.Count());
            ChangeItem(identifier, userInput, quantity);
        }

        /// This function changes the quantity of an item in a list and updates the total cost, taking
        /// into account any applied vouchers or loyalty points.
        /// 
        /// @param identifier A string that can be either "+" or "-" indicating whether the quantity of
        /// the item should be increased or decreased.
        /// @param index The index parameter is used to identify which item in the items dictionary to
        /// modify. It is decremented by 1 in each iteration of the foreach loop until it reaches 1, at
        /// which point the corresponding item is modified.
        /// @param quantity The quantity of the item to be added or removed.
        /// 
        /// @return If the quantity is less than 0, the method returns without making any changes.
        /// Otherwise, the method updates the quantity of the item with the given index and identifier,
        /// and recalculates the total cost after applying any applicable vouchers and loyalty points.
        /// No value is explicitly returned from the method.
        public void ChangeItem(string identifier, int index, int quantity) {
            if (quantity < 0) {
                Console.WriteLine("Invalid Quantity!");
                return;
            }
            foreach (var item in items) {
                if (index == 1) {
                    int newQuantity = identifier == "+" ? item.Value + quantity : item.Value - quantity;
                    if (newQuantity <= 0) {
                        Console.WriteLine("Item removed!");
                        RemoveItem(item.Key);
                    } else if (newQuantity > 50) {
                        Console.WriteLine("Cannot add more than 50!");
                    } else {
                        items[item.Key] = newQuantity;
                        totalCost = CalculateTotalPrice();  //calculate the total cost.
                        foreach (var voucher in currentApplied) {  // apply every voucher again on the new cost.
                            totalCost -= totalCost * voucher.GetDiscountValue();
                        }
                        totalCost -= currentAppliedPoints * LoyalityPoints.GetDiscountValue(); //apply added loyality points.
                    }
                    break;
                }
                index--;
            }
        }

        /// The function allows the user to update the quantity of products in a catalogue by increasing
        /// or decreasing the quantity, or returning to the catalogue.
        public void Updateitems() {
            int userInput;
            do {
                PrintItems();
                Console.WriteLine("1) Increase a Product's Quantity");
                Console.WriteLine("2) Decrease a Product's Quantity");
                Console.WriteLine("3) Go to Catalogue.");
                Console.Write("Choose an option: ");
                int.TryParse(Console.ReadLine(), out userInput);

                switch (userInput) {
                    case 1:
                        ChangeQuantity("+");
                        break;
                    case 2:
                        ChangeQuantity("-");
                        break;
                    case 3:
                        break;
                    default:
                        System.Console.WriteLine("Invalid Input, Please try again!");
                        break;
                }
            } while (userInput != 3);
        }

        public void ClearCart() {
            items.Clear();
            totalCost = 0;
        }

        /// The function applies a voucher discount to the total cost of a purchase if the voucher is
        /// not expired.
        /// 
        /// @param Voucher The Voucher parameter is an object of the Voucher class, which contains
        /// information about a discount voucher, such as its discount value and expiry date.
        public void ApplyVoucher(Voucher voucher) {
            if (!voucher.GetExpiryState()) {
                double tempCost = totalCost;
                totalCost -= totalCost * voucher.GetDiscountValue();
                if (totalCost < 0) {
                    totalCost = 0;
                }
                currentApplied.Add(voucher);
            }
        }

        /// The function calculates the total cost of items with discounts applied.
        /// 
        /// @return The method is returning the total cost of all items in the list after applying any
        /// discounts.
        public double CalculateTotalPrice() {
            totalCost = 0;
            foreach (var item in items) {
                totalCost += item.Key.GetDiscountPrice() * item.Value;
            }
            return totalCost;
        }

        public Dictionary<Product, int> GetProductList() {
            return items;
        }

        /// The function reverts changes made to the total cost by recalculating it.
        public void RevertChanges() {
            totalCost = CalculateTotalPrice();
        }

        public double GetTotalCost() {
            return totalCost;
        }

        public void SetTotalCost(double totalCost) {
            this.totalCost = totalCost;
        }
        public List<Voucher> GetAppliedVouchers() {
            return currentApplied;
        }

        public void SetAppliedPoints(int points) {
            this.currentAppliedPoints = points;
        }

        public int GetAppliedPoints() {
            return currentAppliedPoints;
        }
    }

    /* The `ShoppingCartData` struct is defining a data structure that represents the data of a
    shopping cart. It contains two properties: `items`, which is a list of key-value pairs where the
    key is an object of the `ProductData` class and the value is an integer representing the
    quantity of that product in the cart, and `totalCost`, which is a double representing the total
    cost of all items in the cart. This struct is likely used for data storage and transfer
    purposes, such as when saving or loading shopping cart data from a file or database. */
    public struct ShoppingCartData {
        public List<KeyValuePair<ProductData, int>> items { get; set; }
        public double totalCost { get; set; }
    }
}