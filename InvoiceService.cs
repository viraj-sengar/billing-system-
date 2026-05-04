using System;
using System.Collections.Generic;
using System.Linq;
using billing_system.Domain;
using billing_system.Discounts;
using billing_system.Data;

namespace billing_system.Services
{
    public class InvoiceService
    {
        private readonly BillingDbContext _db;

        public InvoiceService(BillingDbContext db)
        {
            _db = db;
        }

        public Invoice GenerateInvoice(Subscription subscription, decimal usage, IEnumerable<IDiscountPolicy> discounts)
        {
            if (subscription == null) throw new ArgumentNullException(nameof(subscription));

            var baseAmount = subscription.BillingStrategy.Calculate(usage, subscription.Plan);
            var invoice = new Invoice(subscription, baseAmount);

            foreach (var d in discounts ?? Array.Empty<IDiscountPolicy>())
            {
                var (amount, desc) = d.Apply(invoice.Total, usage);
                if (amount > 0)
                {
                    invoice.ApplyDiscount(desc, amount);
                }
            }

         
            _db.Invoices.Add(invoice);
            _db.SaveChanges();

            return invoice;
        }

        public IEnumerable<Invoice> GetInvoicesForTenant(Guid tenantId)
        {
            return _db.Invoices.Where(i => i.Subscription != null && i.Subscription.TenantId == tenantId).ToList();
        }
    }
}
