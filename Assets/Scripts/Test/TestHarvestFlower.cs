using GardeningGame.Items;
using GardeningGame.Plants;
using System;
using UnityEngine;
using UnityEngine.U2D;

public class TestHarvestFlower : Plant, IHarvestable, IAnnual, ICheckSoil, ICheckWeather
{
    public override string plantName => "Test Harvest Flower";

    public override string description => "Test flower";

    public override SpriteAtlas atlas => Resources.Load<SpriteAtlas>("SpriteAtlases/Annuals/Marigold");

    public int seedsToPlant => 3;

    public override FeederType feederType => FeederType.Light;

    public void CheckSoilConditions(GardenTile gardenTile)
    {
        if (gardenTile.nitrogen < 0.0f) TakeDamage(20);
    }

    public void CheckWeatherConditions()
    {
        if (Weather.Instance.currentTemperature < 5.0f) TakeDamage(20);
    }

    public override Sprite GetSprite()
    {
        if (IsDead)
        {
            return age > 5 ? age > 10? age > 20 ?
                atlas.GetSprite("marigold_mature_dead") :
                atlas.GetSprite("marigold_growing_dead") :
                atlas.GetSprite("marigold_seedling_dead") :
                null;
        }
        else
        {
            return age > 5 ? age > 10 ? age > 20 ?
                atlas.GetSprite("marigold_mature") :
                atlas.GetSprite("marigold_growing") :
                atlas.GetSprite("marigold_seedling") :
                null;
        }
    }

    public override void OnDailyEvent()
    {
        CheckSoilConditions(tile);
        CheckWeatherConditions();
    }

    public void OnHarvest()
    {
        Inventory.Instance.AddItem(new TestHarvestFlowerSeed(3));
        Inventory.Instance.AddItem(new TestHarvestFlowerCutFlower(1));
    }

    public override void OnPlant()
    {
        if(!Inventory.Instance.RemoveItem(new TestHarvestFlowerSeed(3)))
        {
            Money.Instance.RemoveFromBalance(5);
        }
    }
}

public class TestHarvestFlowerSeed : Seed
{
    public override int maxStack => 10000;
    public TestHarvestFlowerSeed(int quantity) : base(quantity) { }

    public override string ToString() => quantity > 1 ? "Test Harvest Flower Seeds" : "Test Harvest Flower Seed";
}

public class TestHarvestFlowerCutFlower : Item, IStackable
{
    public int quantity { get; private set; } = 0;

    public int maxStack => 2;

    public TestHarvestFlowerCutFlower(int quantity)
    {
        this.quantity = quantity;
    }

    public bool RemoveFromStack(int amount)
    {
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

    public override string ToString()
    {
        return quantity > 1 ? "Test Harvest Flowers" : "Test Harvest Flower";
    }
}
