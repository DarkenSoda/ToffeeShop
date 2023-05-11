using System;

namespace CS251_A3_ToffeeShop.BalanceClasses {
    public class LoyalityPoints {
        private int points = 0;
        private static double discountValue = 1.2;

        public static void ChangeDiscountValue(double value) {
            discountValue = value;
        }

        public void AddPoints(int points) {
            this.points += points;
        }

        public double RedeemPoints(int points) {
            this.points -= points;
            return discountValue * points;
        }

        public static double GetDiscountValue() {
            return discountValue;
        }

        public int GetPoints() {
            return points;
        }
    }
}