using System;

namespace billing_system.Discounts
{
    public class VolumeDiscount : IDiscountPolicy
    {
        private readonly decimal _threshold;
        private readonly decimal _percentage;

        public VolumeDiscount(decimal threshold, decimal percentage)
        {
            _threshold = threshold;
            _percentage = percentage;
        }

        public (decimal amount, string description) Apply(decimal amountBeforeDiscount, decimal usage = 0)
        {
            if (amountBeforeDiscount >= _threshold || usage >= _threshold)
            {
                var discount = amountBeforeDiscount * _percentage;
                return (discount, $"Volume discount {_percentage:P0} for threshold {_threshold}");
            }

            return (0m, "");
        }
    }
}
