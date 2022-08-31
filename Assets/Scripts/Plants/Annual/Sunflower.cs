using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;
using UnityEngine.U2D;

public class Sunflower : Plant, IHarvestable
{
    public override string plantName => "Sunflower";

    public override string description => "Praise the sun!";

    public override SpriteAtlas atlas => Resources.Load<SpriteAtlas>("SpriteAtlases/Annuals/Sunflower");

    public override FeederType feederType => FeederType.Heavy;

    public override Sprite GetSprite()
    {
        if (IsDead)
        {
            return age > 5 ? age > 10 ? age > 20 ?
                atlas.GetSprite("sunflower_mature_dead") :
                atlas.GetSprite("sunflower_growing_dead") :
                atlas.GetSprite("sunflower_seedling_dead") :
                null;
        }
        else
        {
            return age > 5 ? age > 10 ? age > 20 ?
                atlas.GetSprite("sunflower_mature") :
                atlas.GetSprite("sunflower_growing") :
                atlas.GetSprite("sunflower_seedling") :
                null;
        }
    }

    public override void OnDailyEvent()
    {
        
    }

    public override void OnPlant()
    {
        if (!Inventory.Instance.RemoveItem(new MarigoldSeed(2)))
        {
            Money.Instance.RemoveFromBalance(10);
        }
    }

    public void OnHarvest()
    {
        Inventory.Instance.AddItem(new SunflowerSeed(10));
    }


    public override string SubTypeToString() => "Flower";
}

public class SunflowerSeed : Seed
{
    public SunflowerSeed(int quantity) : base(quantity)
    {
    }

    public override int maxStack => 50;
    
    public override string ToString() => quantity > 1 ? "Sunflower Seeds" : "Sunflower Seed";

}
