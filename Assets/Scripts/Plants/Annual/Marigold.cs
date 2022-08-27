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

    public override void CheckSoilConditions(GardenTile gardenTile)
    {
        throw new NotImplementedException();
    }

    public override void CheckWeatherConditions()
    {
        throw new NotImplementedException();
    }

    public override Sprite GetSprite()
    {
        throw new NotImplementedException();
    }

    public override void OnPlant()
    {
        Debug.Log(string.Format($"I planted {0} seeds", seedsToPlant));
    }

    public void OnHarvest()
    {
        //Item output = new();

    }

    public override string SubTypeToString()
    {
        return "Flower";
    }

    public override string StageToString()
    {
        return base.StageToString();
    }
}
