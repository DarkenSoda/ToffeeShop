using System;


namespace CS251_A3_ToffeeShop.PaymentMethod {
    public class OnDeliveryStrategy : IPaymentMethodStrategy {
        private string emailAdress;
        public OnDeliveryStrategy(string emailAdress) {
            this.emailAdress = emailAdress;
        }

        // Send OTP To EmailAddress
        // Return True if OTP is entered correctly
        // False otherwise
        public bool ValidatePayment() {
            return Authentication.Authenticate(emailAdress);
        }
    }
}