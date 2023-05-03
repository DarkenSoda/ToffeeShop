using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS251_A3_ToffeeShop.Items;
using CS251_A3_ToffeeShop.BalanceClasses;


namespace CS251_A3_ToffeeShop.CartClasses {
    public class ShoppingCart {
        private List<Product> items = new List<Product>();
        private double totalCost = 0;
        private double fixedTotalCost;
        public void AddItem(Product item) {
            items.Add(item);
            totalCost += item.GetDiscountPrice();
        }
        public void RemoveItem(Product item) {
            if (items.Remove(item)) {
                Console.WriteLine("Item Removed Successfully!\n");
            }
        }
        public void Updateitems(Product item) {
            for (int i = 0; i < items.Count(); i++) {
                if (items[i] == item) {
                    items[i] = item;
                } 
            }
            Console.WriteLine("Item not Found!");
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
        public List<Product> GetProductList() {
            return items;
        }
    }
}
