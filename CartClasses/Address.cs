using System;

namespace CS251_A3_ToffeeShop.CartClasses {
    public class Address {
        private string street;
        private string city;
        private string buildingNumber;
        public Address(string street,string city, string buildingNumber) {
            this.street = street;
            this.city = city;
            this.buildingNumber = buildingNumber;
        }
        public string GetAddress() {
            return buildingNumber + ' ' + street + ' ' + city;
        }
    }
}