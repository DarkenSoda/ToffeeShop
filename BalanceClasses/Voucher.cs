using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS251_A3_ToffeeShop.CartClasses;

namespace CS251_A3_ToffeeShop.BalanceClasses {
    public class Voucher {
        private string voucherCode;
        private double discountValue = 0;
        private bool isExpired = false;
        
        public Voucher(string voucherCode, double discountValue, bool isExpired = false) {
            this.voucherCode = voucherCode;
            this.discountValue = discountValue;
            this.isExpired = isExpired;
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
            return voucherCode;
        }
    }

    public struct VoucherData {
        public string voucherCode { get; set; }
        public double discountValue { get; set; }
        public bool isExpired { get; set; }
    }
}