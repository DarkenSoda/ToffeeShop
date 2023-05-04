namespace CS251_A3_ToffeeShop.PaymentMethod {
    public interface IPaymentMethodStrategy {
        public bool ValidatePayment();
    }
}