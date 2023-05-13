using System;

/* This is a C# code defining a namespace `CS251_A3_ToffeeShop.BalanceClasses` which contains a class
`Voucher` and a struct `VoucherData`. */
namespace CS251_A3_ToffeeShop.BalanceClasses {
    /* The Voucher class represents a discount voucher with a unique code, discount value, and expiry
    state. */
    public class Voucher {
        private string? voucherCode;
        private static int voucherNumber = 0;
        private double discountValue = 0;
        private bool isExpired = false;

        /* This is a constructor for the `Voucher` class that takes in two parameters: `discountValue`
        and `isExpired`. It initializes the `discountValue` and `isExpired` fields of the `Voucher`
        object with the values passed in as parameters. It also generates a unique voucher code by
        incrementing a static `voucherNumber` field and appending it to the string "VCH". The
        `isExpired` parameter is optional and defaults to `false` if not provided. */
        public Voucher(double discountValue, bool isExpired = false) {
            this.discountValue = discountValue;
            this.isExpired = isExpired;
            voucherCode = "VCH" + voucherNumber.ToString();
        }

        public static void SetVoucherNumber(int vn) {
            voucherNumber = vn;
        }

        public static int GetVoucherNumber() {
            return voucherNumber;
        }

        public double RedeemVoucher() {
            isExpired = true;
            return discountValue;
        }

        public bool GetExpiryState() {
            return isExpired;
        }

        public void SetDiscountValue(double discountValue) {
            this.discountValue = discountValue;
        }

        public double GetDiscountValue() {
            return discountValue;
        }

        public string GetVoucherCode() {
            if (string.IsNullOrEmpty(voucherCode))
                return "";
            return voucherCode;
        }

        public void SetvoucherCode(string code) {
            this.voucherCode = code;
        }
    }

    /* The `VoucherData` struct defines a data structure that contains information about a discount
    voucher, including its voucher code, discount value, and expiry state. The struct has three
    public properties: `voucherCode`, `discountValue`, and `isExpired`, each with a getter and a
    setter method. This struct can be used to store and pass around voucher data in a more organized
    and structured way. */
    public struct VoucherData {
        public string voucherCode { get; set; }
        public double discountValue { get; set; }
        public bool isExpired { get; set; }
    }
}