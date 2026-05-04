using System;
using billing_system.Data;
using billing_system.Domain;

namespace billing_system.Services
{
    public class TenantService
    {
        private readonly BillingDbContext _db;

        public TenantService(BillingDbContext db)
        {
            _db = db;
        }

        public Tenant CreateTenant(string name)
        {
            var t = new Tenant(name);
            _db.Tenants.Add(t);
            _db.SaveChanges();
            return t;
        }

        public Plan CreatePlan(string name, decimal price)
        {
            var p = new Plan(name, price);
            _db.Plans.Add(p);
            _db.SaveChanges();
            return p;
        }

        public Subscription Subscribe(Tenant tenant, Plan plan, Billing.BillingStrategy billingStrategy)
        {
            var s = new Subscription(tenant, plan, billingStrategy);
            _db.Subscriptions.Add(s);
            _db.SaveChanges();
            return s;
        }
    }
}
