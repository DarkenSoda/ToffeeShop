using System;

namespace CS251_A3_ToffeeShop.BalanceClasses
{
    public class Voucher
    {
        private string? voucherCode;
        private static int voucherNumber = 0;
        private double discountValue = 0;
        private bool isExpired = false;

        public Voucher(double discountValue, bool isExpired = false)
        {
            this.discountValue = discountValue;
            this.isExpired = isExpired;
            voucherCode = "VCH" + voucherNumber.ToString();
        }
        public static void SetVoucherNumber(int vn) {
            voucherNumber = vn;
        }
        public static int GetVoucherNumber()
        {
            return voucherNumber;
        }
        public double RedeemVoucher() {
            isExpired = true;
            return discountValue;
        }


        public bool GetExpiryState()
        {
            return isExpired;
        }

        public void SetDiscountValue(double discountValue)
        {
            this.discountValue = discountValue;
        }

        public double GetDiscountValue()
        {
            return discountValue;
        }

        public string GetVoucherCode()
        {
            if (string.IsNullOrEmpty(voucherCode)) 
                return "";
            return voucherCode;
        }
    }

    public struct VoucherData
    {
        public string voucherCode { get; set; }
        public double discountValue { get; set; }
        public bool isExpired { get; set; }
    }
}