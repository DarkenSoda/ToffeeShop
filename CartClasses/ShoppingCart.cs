using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.BalanceClasses;


namespace CS251_A3_ToffeeShop.CartClasses {
    public class ShoppingCart {
        private Dictionary<Product,int> items = new Dictionary<Product,int>();
        private double totalCost = 0;

        
        public void PrintItems(){
            int i = 1;
            foreach (var kvp in items) {
            Console.WriteLine(i  +  ",  {0}   Quantity: {2}      Cost: {1} L.E", kvp.Key.GetName(),kvp.Key.GetDiscountPrice()*kvp.Value, kvp.Value);
                i++;
            }
             Console.WriteLine("--------------[ Total Cost: {0} L.E ]--------------",CalculateTotalPrice());
        }
        public void AddItem(Product item, int quantity) {
            items.Add(item,quantity);
            totalCost += item.GetDiscountPrice() * quantity;
        }
        public void RemoveItem(Product item) {
           if (!items.ContainsKey(item))
                Console.WriteLine("Item is not Found!");
            else
                items.Remove(item);
        }
        public void ChangeQuantity(string identifier) {
            int userInput;
            do {
                System.Console.Write("Enter Product number: ");
                int.TryParse(Console.ReadLine(), out userInput);
            } while (userInput <= 0 && userInput > items.Count());
            ChangeItem(identifier,userInput);

        }
        public void ChangeItem(string identifier,int index) {
            foreach(var item in items) {
                if (index == 1) {
                    int newQuantity = identifier == "+" ? item.Value + 1 : item.Value - 1;
                    RemoveItem(item.Key);
                    AddItem(item.Key,newQuantity);
                    break;
                }
                index--;
            }
        }
        public void Updateitems() {
            PrintItems();
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1) Increase a Product's Quantity");
            Console.WriteLine("2) Decrease a Product's Quantity");
            Console.WriteLine("3) Go to Catalogue.");
            int userInput;
            int.TryParse(Console.ReadLine(), out userInput);
            do {
            switch(userInput) {
                case 1:
                    ChangeQuantity("+");
                    Updateitems();
                    break;
                case 2:
                    ChangeQuantity("-");
                    Updateitems();
                    break;
                case 3:
                    return;
                default:
                    System.Console.WriteLine("Invalid Input, Please try again!");
                    break;
            }
            } while (userInput != 3);

        }

        public void ClearCart() {
            items.Clear();
        }
        public void ApplyVoucher(Voucher voucher) {
            if (!voucher.GetExpiryState()) {
                double tempCost = totalCost;
                totalCost -= voucher.GetDiscountValue();
                voucher.SetDiscountValue(voucher.GetDiscountValue() - tempCost);
                if (voucher.GetDiscountValue() < 0) {
                    voucher.RedeemVoucher();
                }
                else if (totalCost < 0) {
                    totalCost = 0; 
                }
            }
        }
        public void ApplyLoyalityPoints(LoyalityPoints loyalityPoints, int points) {
            if (points * loyalityPoints.GetDiscountValue() > totalCost) {
                points = (int)(totalCost / loyalityPoints.GetDiscountValue());
            }
            totalCost -= loyalityPoints.RedeemPoints(points);
        }
        public double CalculateTotalPrice() {
            totalCost = 0;
            foreach (var item in items) {
                totalCost += item.Key.GetDiscountPrice() * item.Value;
            }
            return totalCost;
        }
        public Dictionary<Product,int> GetProductList() {
            return items;
        }
        public void RevertChanges() {
            totalCost = CalculateTotalPrice();
        }
    }
}

public struct ShoppingCartData {
    public Dictionary<ProductData, int> items { get; set; }
    public double totalCost { get; set; }
}