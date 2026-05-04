using System;

namespace billing_system.Domain
{
    public class Subscription
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Tenant Tenant { get; private set; }
        public Guid TenantId { get; private set; }
        public Plan Plan { get; private set; }
        public Guid PlanId { get; private set; }
        
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public Billing.BillingStrategy BillingStrategy { get; private set; }
        public string BillingStrategyName { get; private set; }
        public DateTime StartDate { get; private set; } = DateTime.UtcNow;
        protected Subscription() { }

        public Subscription(Tenant tenant, Plan plan, Billing.BillingStrategy billingStrategy)
        {
            Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
            TenantId = tenant.Id;
            Plan = plan ?? throw new ArgumentNullException(nameof(plan));
            PlanId = plan.Id;
            BillingStrategy = billingStrategy ?? throw new ArgumentNullException(nameof(billingStrategy));
            BillingStrategyName = billingStrategy.GetType().Name;
        }
    }
}
