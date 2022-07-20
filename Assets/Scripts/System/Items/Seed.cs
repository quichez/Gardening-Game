using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Items;

public abstract class Seed : Item, IStackable, IQuality, IQualityDegrade
{
    public int quantity { get; private set; }

    public int quality { get { return _quality; } private set { _quality = Mathf.Clamp(value, 0, 5); } }
    private int _quality;

    public abstract float timeToDegrade { get; protected set; }

    public float degradeTimer { get; private set; } = 0.0f;

    protected Seed(string name, int quality, int quantity) : base(name)
    {
        this.quantity = quantity;
        _quality = quality;
    }

    public void AddToDegradeTimer()
    {
        degradeTimer += Time.deltaTime;
        if(degradeTimer >= timeToDegrade)
        {
            quality -= 1;
        }
    }
}
