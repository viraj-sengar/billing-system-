using System;

namespace billing_system.Billing
{
    public class FlatRateBilling : BillingStrategy
    {
        private readonly decimal _amount;

        public FlatRateBilling(decimal amount)
        {
            _amount = amount;
        }

        public override decimal Calculate(decimal usage, Domain.Plan plan)
        {
            return _amount;
        }
    }
}
