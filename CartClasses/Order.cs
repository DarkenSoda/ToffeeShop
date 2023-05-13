using System;

/* This code defines the `Order` class and `OrderData` struct in the `CS251_A3_ToffeeShop.CartClasses`
namespace. */
namespace CS251_A3_ToffeeShop.CartClasses {

    /* The `public enum OrderState` is defining an enumeration type called `OrderState` with four
    possible values: `Ordered`, `Delivered`, `inDelivery`, and `Canceled`. This enumeration type is
    used to represent the current state of an order in the `Order` class. */
    public enum OrderState {
        Ordered, Delivered, inDelivery, Canceled
    }

    /* The Order class represents an order with a shopping cart, delivery address, order status, and
    date/time information, and provides methods to update and retrieve this information. */
    public class Order {
        private OrderState orderStatus;
        private ShoppingCart shoppingCart;
        private Address deliveryAddress;
        private string dateTime;

        /* This is a constructor for the `Order` class that takes in a `ShoppingCart` object and an
        `Address` object as parameters. It initializes the `shoppingCart` and `deliveryAddress`
        fields of the `Order` object with the values passed in as parameters. It also sets the
        `orderStatus` field to `OrderState.Ordered` and sets the `dateTime` field to the current
        date and time in the format "dd/MM/yyyy hh:mm tt". */
        public Order(ShoppingCart shoppingCart, Address deliveryAddress) {
            this.shoppingCart = new ShoppingCart(shoppingCart);
            this.deliveryAddress = deliveryAddress;
            this.orderStatus = OrderState.Ordered;
            this.dateTime = DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy hh:mm tt");
        }

        /// This function allows the user to update the state of an order by selecting from a list of
        /// options.
        public void UpdateState() {
            Console.WriteLine("1) Ordered\n2) Delivered\n3) In Delivery\n4) Canceled\n5) Go Back");
            Console.Write("Enter a State to change:");
            string? choice;
            while (true) {
                choice = Console.ReadLine();
                if (choice == "1") {
                    orderStatus = OrderState.Ordered;
                    break;
                }
                if (choice == "2") {
                    orderStatus = OrderState.Delivered;
                    break;
                }
                if (choice == "3") {
                    orderStatus = OrderState.inDelivery;
                    break;
                }
                if (choice == "4") {
                    orderStatus = OrderState.Canceled;
                    break;
                }
                if (choice == "5") {
                    break;
                }

                Console.WriteLine("Invalid choice! Try again!");
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

    /* The `OrderData` struct is defining a data structure that contains information about an order,
    including its current state (`orderState`), the shopping cart data (`shoppingCartData`), the
    delivery address (`deliveryAddress`), and the date/time of the order (`dateTime`). 
    This struct is used for storing and retrieving order information in a database. */
    public struct OrderData {
        public OrderState orderState { get; set; }
        public ShoppingCartData shoppingCartData { get; set; }
        public Address deliveryAddress { get; set; }
        public string dateTime { get; set; }
    }
}