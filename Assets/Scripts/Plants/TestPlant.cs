using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;
using UnityEngine.U2D;

public class TestPlant : Annual
{
    public TestPlant() { }
    public TestPlant(GardenTile gardenTile) : base(gardenTile) { }

    public override string plantName => "Test Plant";

    public override string description => "New Test Plant";

    public override int cost => 10;
    public override int daysToGerminate => 7;

    public override float minimumTemperatureToGerminate => 0.0f;

    public override float moistureRequiredForGermination => 0.1f;

    public override float moistureToleranceForGermination => 1.0f;

    public override int daysToFirstLeaves => 14;

    public override int daysToMaturity => 60;

    public override SpriteAtlas atlas => throw new System.NotImplementedException();

    public override void CheckSoilConditions(GardenTile gardenTile)
    {
        if (HasFirstLeaves)
        {
            if (gardenTile.nitrogen < 0.1) TakeDamage(1);
            if (gardenTile.phosphorus < 0.1) TakeDamage(1);
            if (gardenTile.potassium < 0.1) TakeDamage(2);
            if (gardenTile.soilMoisture > 0.8f || gardenTile.soilMoisture < 0.0f) TakeDamage(1);
        }
    }

    public override void CheckWeatherConditions()
    {
        if (HasFirstLeaves)
        {
            if (Weather.Instance.currentTemperature <= 29.0f)
            {
                int dmg = 30 - (int) Weather.Instance.currentTemperature;
                dmg = Mathf.Min(dmg, 20);
            }
        }
    }

    public override Sprite GetSprite()
    {
        throw new System.NotImplementedException();
    }

    public override string SubTypeToString()
    {
        return "testplant";
    }
}