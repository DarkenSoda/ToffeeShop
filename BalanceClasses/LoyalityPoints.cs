using System;

/* This is a C# code defining a class `LoyalityPoints` in the namespace
`CS251_A3_ToffeeShop.BalanceClasses`. The class has private fields `points` and `discountValue` and
public methods `ChangeDiscountValue`, `AddPoints`, `RedeemPoints`, `GetDiscountValue`, and
`GetPoints`. These methods allow for adding and redeeming loyalty points, changing the discount
value, and getting the current points and discount value. */
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

        /// The function RedeemPoints subtracts the given points from the current points and returns the
        /// discount value multiplied by the given points.
        /// 
        /// @param points The parameter "points" represents the number of points that a customer wants
        /// to redeem for a discount. The method subtracts this number of points from the total points
        /// available and returns the value of the discount that the customer is eligible for based on
        /// the discountValue variable.
        /// 
        /// @return The method is returning the discount value multiplied by the number of points that
        /// were redeemed.
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