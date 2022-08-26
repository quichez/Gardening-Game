using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;
using UnityEngine.U2D;
using System;

public class Marigold : Plant, IPlantable, IHarvestable, IGrowFromSeed
{
    public override string plantName => "Marigold";

    public override string description => "Warm colors, symbolize death, great for tomatoes!";

    public override SpriteAtlas atlas => Resources.Load<SpriteAtlas>("SpriteAtlases/Annuals/Marigold");

    public int seedsToPlant => 3;

    public int seedsOnHarvest => 10;

    public bool IsSeed => !IsPlanted;

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

    public void OnPlant()
    {
        Debug.Log(string.Format($"I planted {0} seeds", seedsToPlant));
    }

    public override void OnHarvest()
    {
        base.OnHarvest();
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
