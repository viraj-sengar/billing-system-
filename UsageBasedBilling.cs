using System;

namespace billing_system.Billing
{
    public class UsageBasedBilling : BillingStrategy
    {
        private readonly decimal _ratePerUnit;

        public UsageBasedBilling(decimal ratePerUnit)
        {
            _ratePerUnit = ratePerUnit;
        }

        public override decimal Calculate(decimal usage, Domain.Plan plan)
        {
            return usage * _ratePerUnit;
        }
    }
}
