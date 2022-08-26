using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Items;

public abstract class Seed : Item, IStackable //, IQuality, IQualityDegrade -- future maybe
{
    public int quantity { get; protected set; }
    public abstract int maxStack { get; }

    protected Seed(int quantity)
    {
        this.quantity = quantity;
    }

    public bool RemoveFromStack(int amount)
    {
        if(quantity-amount < 0) return false;
        quantity = Mathf.Max(0, quantity - amount);
        return true;
    }

    public void AddToStack(int amount)
    {
        quantity = Mathf.Min(maxStack, quantity + amount);
    }

    public void Fill()
    {
        quantity = maxStack;
    }    
}
