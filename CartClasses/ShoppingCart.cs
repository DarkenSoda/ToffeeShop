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
            Console.WriteLine(i+"Key = {0}, Value = {1}", kvp.Key, kvp.Value);
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
        public void Updateitems(Product oldItem ,Product newItem, int quantity) {
            if (!items.ContainsKey(oldItem))
                Console.WriteLine("Item is not Found!");
            else {
                items.Remove(oldItem);
                items.Add(newItem,quantity);
            }
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
                points = (int)(totalCost / 1.2);
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
