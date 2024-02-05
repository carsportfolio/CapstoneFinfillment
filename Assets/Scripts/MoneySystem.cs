using System;

//This script is a money class used to keep track of money.

public class Money
{
    private decimal amount;

    public Money(decimal initialAmount)
    {
        amount = initialAmount;
    }

    public void Add(decimal value)
    {
        amount += value;
    }

    public void Subtract(decimal value)
    {
        if (value > amount)
        {
            throw new InvalidOperationException("INSUFFICIENT FUNDS."); // if throws an error because you cant due to the int
        }
        amount -= value; // otherwise take it
    }

    public decimal GetAmount() // display amount of money
    {
        return amount;
    }
}
