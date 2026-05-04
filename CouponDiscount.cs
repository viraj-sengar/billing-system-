using System;

namespace billing_system.Discounts
{
    public class CouponDiscount : IDiscountPolicy
    {
        private readonly decimal _value;
        private readonly string _code;

        public CouponDiscount(string code, decimal value)
        {
            _code = code ?? throw new ArgumentNullException(nameof(code));
            _value = value;
        }

        public (decimal amount, string description) Apply(decimal amountBeforeDiscount, decimal usage = 0)
        {
            var discount = Math.Min(_value, amountBeforeDiscount);
            return (discount, $"Coupon {_code} applied");
        }
    }
}
