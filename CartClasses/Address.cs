using System;

/* This code defines a class called `Address` in the `CartClasses` namespace of a C# project called
`CS251_A3_ToffeeShop`. The `Address` class has three properties (`street`, `city`, and
`buildingNumber`) and a constructor that takes in values for these properties. It also has a method
called `GetAddress()` that returns a formatted string of the address. */
namespace CS251_A3_ToffeeShop.CartClasses {
    public class Address {
        public string street { get; set; }
        public string city { get; set; }
        public string buildingNumber { get; set; }

        public Address(string street, string city, string buildingNumber) {
            this.street = street;
            this.city = city;
            this.buildingNumber = buildingNumber;
        }

        public string GetAddress() {
            return buildingNumber + ' ' + street + ' ' + city;
        }
    }
}