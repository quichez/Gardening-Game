using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Items;

public abstract class Seed : Item, IStackable//, IQuality, IQualityDegrade -- future maybe
{
    public int quantity { get; private set; }
    public virtual int maxStack => 10000;

    protected Seed(int quantity)
    {
        this.quantity = quantity;
    }

    public void AddToStack(int amount)
    {
        quantity = Mathf.Min(maxStack, quantity + amount);
    }
}
