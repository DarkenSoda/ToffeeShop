using System;

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