using System;

namespace billing_system.Domain
{
    public class Payment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Invoice Invoice { get; private set; }
        public Guid InvoiceId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime PaidAt { get; private set; } = DateTime.UtcNow;
        protected Payment() { }

        public Payment(Invoice invoice, decimal amount)
        {
            Invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
            InvoiceId = invoice.Id;
            Amount = amount;
        }
    }
}
