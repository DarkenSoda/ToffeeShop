/* This code defines an interface named `IPaymentMethodStrategy` inside the `PaymentMethod` namespace.
The interface has one method named `ValidatePayment` which returns a boolean value. This interface
can be implemented by different payment methods to provide a common way of validating payments. */
namespace CS251_A3_ToffeeShop.PaymentMethod {
    public interface IPaymentMethodStrategy {
        public bool ValidatePayment();
    }
}