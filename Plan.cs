using System;

namespace billing_system.Domain
{
    public class Plan
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        protected Plan() { }

        public Plan(string name, decimal price)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
        }
    }
}
