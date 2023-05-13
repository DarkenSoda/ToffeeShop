using System;
using System.Net;
using System.Net.Mail;

/* The code defines a class named `Authentication` in the namespace `CS251_A3_ToffeeShop`. The class
has a private constructor and a public static property named `Instance` that returns a singleton
instance of the `Authentication` class. The class also has a public static method named
`Authenticate` that takes an email address as input, generates a random one-time password (OTP),
sends an email containing the OTP to the provided email address, prompts the user to enter the OTP,
and returns `true` if the user enters the correct OTP, otherwise `false`. The class also has a
private method named `GenerateRandomNumber` that generates a random integer between 100000 and
999999. */
namespace CS251_A3_ToffeeShop {
    public sealed class Authentication {
        private static readonly Lazy<Authentication> _instance = new Lazy<Authentication>(() => new Authentication());

        public static Authentication Instance { get { return _instance.Value; } }

        private Authentication() { }

        public static bool Authenticate(string emailAdress) {
            // Generate random number
            int OTP = Instance.GenerateRandomNumber();

            try {
                // Create smtp connection and send Message
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                MailMessage message = new MailMessage();
                message.From = new MailAddress("fcai.toffeeshop2@gmail.com");
                message.To.Add(new MailAddress(emailAdress));
                message.Subject = "Your verifcation OTP from Toffee Shop!";
                message.Body = $"<div>Your one time password verifcation is <h2>{OTP}</h2></div>";
                message.IsBodyHtml = true;

                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("fcai.toffeeshop2@gmail.com", "ovftmwaxchqrwtgx");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(message);
            } catch {
                Console.WriteLine("Failed to send Verification Code!");
                return false;
            }

            // Validate user Input
            int input;
            Console.WriteLine("Please Check your spam if you can't find a message in your inbox!");
            Console.Write("Please Enter Verification Code sent to the provided email address: ");
            int.TryParse(Console.ReadLine(), out input);

            return input == OTP;
        }

        private int GenerateRandomNumber() {
            Random rnd = new Random();
            return rnd.Next(100000, 1000000);
        }
    }
}