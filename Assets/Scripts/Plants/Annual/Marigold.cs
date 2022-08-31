using GardeningGame.Items;
using GardeningGame.Plants;
using System;
using UnityEngine;
using UnityEngine.U2D;

public class Marigold : Plant, IHarvestable, IAnnual
{
    public override string plantName => "Marigold";

    public override string description => "Warm colors, symbolize death, great for tomatoes!";

    public override SpriteAtlas atlas => Resources.Load<SpriteAtlas>("SpriteAtlases/Annuals/Marigold");

    public int seedsToPlant => 3;

    public override FeederType feederType => FeederType.Light;

    public override Sprite GetSprite()
    {
        throw new NotImplementedException();
    }

    public override void OnPlant()
    {
        if (!Inventory.Instance.RemoveItem(new MarigoldSeed(3)))
        {
            Money.Instance.RemoveFromBalance(2);
        }
    }

    public void OnHarvest()
    {
        Inventory.Instance.AddItem(new MarigoldSeed(5));
    }

    public override string SubTypeToString()
    {
        return "Flower";
    }

    public override string StageToString()
    {
        return base.StageToString();
    }

    public override void OnDailyEvent()
    {
        throw new NotImplementedException();
    }
}

public class MarigoldSeed : Seed
{
    public MarigoldSeed(int quantity) : base(quantity)
    {
    }

    public override int maxStack => throw new NotImplementedException();

    public override string ToString() => quantity > 1 ? "Marigold Seeds" : "Marigold Seed";
}

