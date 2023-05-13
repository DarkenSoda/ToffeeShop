/* This code defines a namespace `CS251_A3_ToffeeShop.BalanceClasses` and a struct
`BalanceClassesStruct` with two properties: `voucherValueList` which is a list of double values and
`loyalityPointsValue` which is a double value. This struct can be used to store and manage balance
information related to vouchers and loyalty points in a toffee shop. */
namespace CS251_A3_ToffeeShop.BalanceClasses {
    public struct BalanceClassesStruct {
        public List<double> voucherValueList { get; set; }
        public double loyalityPointsValue { get; set; }
    }
}