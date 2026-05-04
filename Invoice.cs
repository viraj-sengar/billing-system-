using System;
using System.Collections.Generic;

namespace billing_system.Domain
{
    public class Invoice
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Subscription Subscription { get; private set; }
        public Guid SubscriptionId { get; private set; }
        public InvoiceStatus Status { get; private set; } = InvoiceStatus.Draft;
        public decimal Subtotal { get; private set; }
        public decimal Total { get; private set; }
        public List<string> Notes { get; } = new List<string>();
        protected Invoice() { }

        public Invoice(Subscription subscription, decimal subtotal)
        {
            Subscription = subscription ?? throw new ArgumentNullException(nameof(subscription));
            SubscriptionId = subscription.Id;
            Subtotal = subtotal;
            Total = subtotal;
        }

        public void ApplyDiscount(string description, decimal amount)
        {
            if (Status != InvoiceStatus.Draft)
                throw new InvalidOperationException("Discounts can only be applied while invoice is Draft.");

            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));

            Total -= amount;
            Notes.Add(description);
        }

        public void FinalizeInvoice()
        {
            if (Status != InvoiceStatus.Draft)
                throw new InvalidOperationException("Only Draft invoices can be finalized.");

            // lock core data
            Status = InvoiceStatus.Finalized;
        }

        public void MarkPaid()
        {
            if (Status != InvoiceStatus.Finalized)
                throw new InvalidOperationException("Only Finalized invoices can be marked as Paid.");

            Status = InvoiceStatus.Paid;
        }
    }
}
