using System;

/* This is a C# code defining a class called `OnDeliveryStrategy` that implements the
`IPaymentMethodStrategy` interface. It has a constructor that takes an email address as a parameter
and a method called `ValidatePayment` that sends an OTP (One-Time Password) to the email address and
returns true if the OTP is entered correctly, false otherwise. The namespace
`CS251_A3_ToffeeShop.PaymentMethod` indicates that this class is part of a larger project or
solution called "ToffeeShop" and is located in the "PaymentMethod" folder or namespace. */
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