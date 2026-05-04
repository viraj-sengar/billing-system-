using System;

namespace billing_system.Billing
{
    public abstract class BillingStrategy
    {
        public abstract decimal Calculate(decimal usage, Domain.Plan plan);
    }
}
