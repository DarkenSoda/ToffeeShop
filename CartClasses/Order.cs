using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS251_A3_ToffeeShop.CartClasses {

    public enum OrderState {
        Ordered, Delivered, inDelivery, Canceled
    }

    public class Order {
        private OrderState orderStatus;
        private ShoppingCart shoppingCart;
        private Address deliveryAddress;
        private string dateTime;
        public Order(ShoppingCart shoppingCart, Address deliveryAddress) {

            this.shoppingCart = new ShoppingCart(shoppingCart);
            this.deliveryAddress = deliveryAddress;
            this.orderStatus = OrderState.Ordered;
            this.dateTime = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy hh:mm tt");
        }
        public void UpdateState() {
            Console.WriteLine("Enter a State to change:");
            Console.WriteLine("1) Ordered.\n2) Delivered.\n3) In Delivery.\n4) Canceled.");
            string? choice;
            while (true) {
                choice = Console.ReadLine();
                if (choice == "1") {
                    orderStatus = OrderState.Ordered;
                    break;
                } else if (choice == "2") {
                    orderStatus = OrderState.Delivered;
                    break;
                } else if (choice == "3") {
                    orderStatus = OrderState.inDelivery;
                    break;
                } else if (choice == "4") {
                    orderStatus = OrderState.Canceled;
                    break;
                } else {
                    System.Console.WriteLine("Invalid choice! Try again!");
                }
            }

        }
        public ShoppingCart GetOrderShoppingCart() {
            return shoppingCart;
        }
        public OrderState GetOrderState() {
            return orderStatus;
        }
        public string GetDateTime() {
            if (dateTime == null) return "";
            else
                return dateTime;
        }
        public void SetAddress(Address address) {
            deliveryAddress = address;
        }
        public Address GetDeliveryAddress() {
            return deliveryAddress;
        }
        public void SetOrderStatue(OrderState state) {
            orderStatus = state;
        }
        public void SetDateTime(string cdateTime) {
            dateTime = cdateTime;
        }
    }

    public struct OrderData {
        public OrderState orderState { get; set; }
        public ShoppingCartData shoppingCartData { get; set; }
        public Address deliveryAddress { get; set; }
        public string dateTime { get; set; }
    }
}