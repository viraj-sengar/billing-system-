using System;
using billing_system.Domain;
using billing_system.Billing;
using billing_system.Discounts;
using billing_system.Services;

namespace billing_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var tenant = new Tenant("ACME Corp");
            var plan = new Plan("Standard", 0m);

           
            var billingStrategy = new UsageBasedBilling(0.10m);
            var subscription = new Subscription(tenant, plan, billingStrategy);

            var discounts = new IDiscountPolicy[]
            {
                new SeasonalDiscount(0.1m),
                new CouponDiscount("WELCOME", 5m)
            };

            var service = new InvoiceService();
            var invoice = service.GenerateInvoice(subscription, usage: 150m, discounts: discounts);

            Console.WriteLine($"Invoice {invoice.Id}");
            Console.WriteLine($"Status: {invoice.Status}");
            Console.WriteLine($"Subtotal: {invoice.Subtotal:C}");
            Console.WriteLine($"Total after discounts: {invoice.Total:C}");

            invoice.FinalizeInvoice();
            Console.WriteLine($"Status after finalize: {invoice.Status}");

            invoice.MarkPaid();
            Console.WriteLine($"Status after payment: {invoice.Status}");
        }
    }
}
