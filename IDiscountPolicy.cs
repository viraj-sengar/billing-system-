namespace billing_system.Discounts
{
    public interface IDiscountPolicy
    {
      
        (decimal amount, string description) Apply(decimal amountBeforeDiscount, decimal usage = 0);
    }
}
