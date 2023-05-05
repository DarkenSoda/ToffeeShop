using System;
using System.Net;
using System.Net.Mail;

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
            // Generate random number
            int OTP = GenerateRandomNumber();

            // Create smtp connection and send Message
            // Note: Suffering
            
            // Validate user Input
            int input;
            Console.Write("Please Enter OTP sent to the provided email address: ");
            int.TryParse(Console.ReadLine(), out input);


            return input == OTP;
        }

        private int GenerateRandomNumber() {
            Random rnd = new Random();

            return rnd.Next(100000, 1000000);
        }
    }
}