using System;

namespace billing_system.Domain
{
    public class Tenant
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        // EF Core requires a parameterless constructor for materialization
        protected Tenant() { }

        public Tenant(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
