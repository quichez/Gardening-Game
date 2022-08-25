using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;
using UnityEngine.U2D;
using System;

public class Marigold : Annual
{
    public override Seed seedType => new MarigoldSeed(2);

    public override int cost => 1;

    public override int daysToGerminate => 1;

    public override float minimumTemperatureToGerminate => 0;

    public override float moistureRequiredForGermination => 0.0f;

    public override float moistureToleranceForGermination => 20f;

    public override int daysToFirstLeaves => 1;

    public override int daysToMaturity => 1;

    public override string plantName => "Marigold";

    public override string description => "One of the best companion plants for vegetable gardens.";    

    public override SpriteAtlas atlas => Resources.Load<SpriteAtlas>("SpriteAtlases/Annuals/Marigold");

    public override void CheckSoilConditions(GardenTile gardenTile)
    {
        if(IsGerminated && !HasFirstLeaves)
        {
            if (gardenTile.soilMoisture < 0.5f  || gardenTile.soilMoisture > 1.1f) TakeDamage(10);
        }
        if (HasFirstLeaves)
        {
            if (gardenTile.nitrogen < 0.1) TakeDamage(1);
            if (gardenTile.phosphorus < 0.1) TakeDamage(1);
            if (gardenTile.potassium < 0.1) TakeDamage(2);
            if (gardenTile.soilMoisture > 1.1f || gardenTile.soilMoisture < 0.2f) TakeDamage(1);
        }
    }

    public override void CheckWeatherConditions()
    {
        if(!HasFirstLeaves)
        {
            if (Weather.Instance.currentTemperature <= 32) TakeDamage(20);            
        }
    }

    public override Sprite GetSprite()
    {       
        if (IsDead)
        {
            return IsGerminated ? HasFirstLeaves ? IsMature ? 
                atlas.GetSprite("marigold_mature_dead") :
                atlas.GetSprite("marigold_growing_dead") :
                atlas.GetSprite("marigold_seedling_dead") :
                null;
        }
        else
        {
            return IsGerminated ? HasFirstLeaves ? IsMature ? 
                atlas.GetSprite("marigold_mature") :
                atlas.GetSprite("marigold_growing") :
                atlas.GetSprite("marigold_seedling") :
                null;
        }
        
    }

    public override string SubTypeToString()
    {
        return "Flower";
    }

    public override void OnHarvest()
    {
        if(IsMature)
            Inventory.Instance.AddItem(new MarigoldSeed(2));
    }
}
