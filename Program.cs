using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using billing_system.Domain;
using billing_system.Billing;
using billing_system.Discounts;
using billing_system.Services;
using billing_system.Data;

namespace billing_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<BillingDbContext>()
                .UseInMemoryDatabase("BillingDb")
                .Options;

            using var db = new BillingDbContext(options);

            
            var tenantService = new TenantService(db);
            var invoiceService = new InvoiceService(db);

            
            var plan = tenantService.CreatePlan("Standard", 0m);

           
            var tenant = tenantService.CreateTenant("ACME Corp");
            var billingStrategy = new UsageBasedBilling(0.10m);
            var subscription = tenantService.Subscribe(tenant, plan, billingStrategy);

            
            var discounts = new IDiscountPolicy[]
            {
                new SeasonalDiscount(0.1m),
                new CouponDiscount("WELCOME", 5m)
            };

           
            var invoice = invoiceService.GenerateInvoice(subscription, usage: 150m, discounts: discounts);

            Console.WriteLine($"Invoice {invoice.Id}");
            Console.WriteLine($"Status: {invoice.Status}");
            Console.WriteLine($"Subtotal: {invoice.Subtotal:C}");
            Console.WriteLine($"Total after discounts: {invoice.Total:C}");

            invoice.FinalizeInvoice();
            db.SaveChanges();
            Console.WriteLine($"Status after finalize: {invoice.Status}");

            var payment = new Payment(invoice, invoice.Total);
            db.Payments.Add(payment);
            invoice.MarkPaid();
            db.SaveChanges();

            Console.WriteLine($"Status after payment: {invoice.Status}");

            var invoices = db.Invoices.Include(i => i.Subscription).Where(i => i.Subscription.TenantId == tenant.Id).ToList();
            Console.WriteLine($"Billing history for {tenant.Name}: {invoices.Count} invoices");
        }
    }
}
