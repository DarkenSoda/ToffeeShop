using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.BalanceClasses;


namespace CS251_A3_ToffeeShop.CartClasses {
    public class ShoppingCart {
        private Dictionary<Product, int> items = new Dictionary<Product, int>();
        private List<Voucher> currentApplied = new List<Voucher>();
        private double totalCost = 0;
        private int currentAppliedPoints = 0;
        public ShoppingCart() { }
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

        public void AddItem(Product item, int quantity) {
            if (items.ContainsKey(item)) {
                items[item] += quantity;
            } else {
                items.Add(item, quantity);
            }
            totalCost += item.GetDiscountPrice() * quantity;
        }

        public void RemoveItem(Product item) {
            if (!items.ContainsKey(item))
                Console.WriteLine("Item is not Found!");
            else
                items.Remove(item);
        }

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
                        foreach(var voucher in currentApplied) {  // apply every voucher again on the new cost.
                            totalCost -= totalCost * voucher.GetDiscountValue();
                        }
                        totalCost -= currentAppliedPoints * LoyalityPoints.GetDiscountValue(); //apply added loyality points.
                    }
                    break;
                }
                index--;
            }
        }

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
}

public struct ShoppingCartData {
    public List<KeyValuePair<ProductData, int>> items { get; set; }
    public double totalCost { get; set; }
}