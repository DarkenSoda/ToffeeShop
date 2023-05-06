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
        private double fixedTotalCost;
        
        public void PrintItems(){
            int i = 1;
            foreach (var kvp in items) {
            Console.WriteLine(i  +  ",  {0}   {1} L.E   Quantity: {2}", kvp.Key.GetName(),kvp.Key.GetPrice(), kvp.Value);
                i++;
            }
        }
        public void AddItem(Product item, int quantity) {
            items.Add(item,quantity);
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
            System.Console.WriteLine("----------------------------");
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
                    break;
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
            fixedTotalCost = totalCost;   //price before changing as if we want to cancel the process.
            if (!voucher.GetExpiryState()) {
                double fixedPrecentage = 0.2 * totalCost;   //the fixed precentage that will be the limit.
               if (voucher.GetDiscountValue() > fixedPrecentage) {
                    double newValue = voucher.GetDiscountValue() - fixedPrecentage;
                    voucher.SetDiscountValue(newValue);
                    totalCost -= fixedPrecentage;
               }
               else {
                totalCost -= voucher.RedeemVoucher();
               }
            }
            else {
                Console.WriteLine("Voucher Has Expired!");
            }
        }
        public void ApplyLoyalityPoints(LoyalityPoints loyalityPoints, int points) {
            if (points * loyalityPoints.GetDiscountValue() > totalCost) {
                points = (int)(totalCost / loyalityPoints.GetDiscountValue());
            }
            totalCost -= loyalityPoints.RedeemPoints(points);
        }
        public double CalculateTotalPrice() {
            return totalCost;
        }
        public Dictionary<Product,int> GetProductList() {
            return items;
        }
    }
}