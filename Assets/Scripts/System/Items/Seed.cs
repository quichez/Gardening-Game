using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Items;

public abstract class Seed : Item, IStackable//, IQuality, IQualityDegrade -- future maybe
{
    public int quantity { get; private set; }
    public virtual int maxStack => 10000;

    protected Seed(int quantity = 0)
    {
        this.quantity = quantity;
    }

    public void AddToStack(int amount)
    {
        quantity = Mathf.Min(maxStack, quantity + amount);
    }
    
    public bool RemoveFromStack(int amount)
    {
        quantity = Mathf.Max(0, quantity - amount);
        return !(quantity == 0);
    }
}
