using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS251_A3_ToffeeShop.CartClasses {
    
    public enum OrderState {
        Ordered, Delivered, inDelivery, Canceled
    }
    public class Order {
        private int OrderCounter = 0;
        private string? orderID;
        private OrderState orderStatus;
        private ShoppingCart? shoppingCart;
        private Address? deliveryAddress;
        private string? dateTime;
        public Order(ShoppingCart shoppingCart, Address deliveryAddress) {
            this.shoppingCart = shoppingCart;
            this.orderID = "OR"+ Convert.ToString(OrderCounter++); 
            this.deliveryAddress = deliveryAddress;
            this.orderStatus = OrderState.Ordered;
            this.dateTime = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy hh:mm tt");
        }
        public void UpdateState() {
            Console.WriteLine("Enter a State to change:");
            Console.WriteLine("1) Ordered.\n2) Delivered.\n3) In Delivery.\n4) Canceled.");
            string? choice;
            while(true) {
                choice = Console.ReadLine();
                if (choice == "1") {
                    orderStatus = OrderState.Ordered;
                    break;
                }
                else if (choice == "2") {
                    orderStatus = OrderState.Delivered;
                    break;
                }
                else if (choice == "3") {
                    orderStatus = OrderState.inDelivery;
                    break;
                }
                else if (choice == "4") {
                    orderStatus = OrderState.Canceled;
                    break;
                }
                else {
                    System.Console.WriteLine("Invalid choice! Try again!");
                }
            }
            
        }
        public OrderState GetOrderState() {
            return orderStatus;
        }
        public string GetDateTime(){
            if (dateTime == null) return "";
            else
                return dateTime;
        }
    }
}