using System;
using System.Collections.Generic;

namespace billing_system.Billing
{
    public class Tier
    {
        public decimal UpTo { get; set; } 
        public decimal Rate { get; set; }
    }

    public class TieredBilling : BillingStrategy
    {
        private readonly List<Tier> _tiers;

        public TieredBilling(IEnumerable<Tier> tiers)
        {
            _tiers = new List<Tier>(tiers);
            _tiers.Sort((a, b) => a.UpTo.CompareTo(b.UpTo));
        }

        public override decimal Calculate(decimal usage, Domain.Plan plan)
        {
            decimal remaining = usage;
            decimal total = 0m;
            decimal lowerBound = 0m;

            foreach (var tier in _tiers)
            {
                var slab = Math.Min(remaining, tier.UpTo - lowerBound);
                if (slab <= 0) break;
                total += slab * tier.Rate;
                remaining -= slab;
                lowerBound = tier.UpTo;
            }

            if (remaining > 0)
            {
                var lastRate = _tiers.Count > 0 ? _tiers[^1].Rate : 0m;
                total += remaining * lastRate;
            }

            return total;
        }
    }
}
