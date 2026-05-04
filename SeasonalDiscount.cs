using System;

namespace billing_system.Discounts
{
    public class SeasonalDiscount : IDiscountPolicy
    {
        private readonly decimal _percentage; // e.g. 0.10 for 10%

        public SeasonalDiscount(decimal percentage)
        {
            if (percentage < 0 || percentage > 1) throw new ArgumentOutOfRangeException(nameof(percentage));
            _percentage = percentage;
        }

        public (decimal amount, string description) Apply(decimal amountBeforeDiscount, decimal usage = 0)
        {
            var discount = amountBeforeDiscount * _percentage;
            return (discount, $"Seasonal {_percentage:P0} off");
        }
    }
}
