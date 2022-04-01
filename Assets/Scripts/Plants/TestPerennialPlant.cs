using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;
public class TestPerennialPlant : Plant
{
    public override string plantName => "Test Perennial";

    public override string description => "Test Perennial Plant";

    public override Sprite sprite => Resources.Load<Sprite>("Sprites/TestPlants/TestPerennial");

    public TestPerennialPlant(GardenTile gardenTile) : base(gardenTile) { }

    public override void CheckSoilConditions(GardenTile gardenTile)
    {
        if(gardenTile.nitrogen < 0.3f)
        {
            TakeDamage(2);
        }
        if(gardenTile.phosphorus < 0.4f)
        {
            TakeDamage(1);
        }
    }

    public override void CheckWeatherConditions()
    {
        if(Weather.Instance.currentTemperature < 30.0f)
        {
            TakeDamage(5);
        }
    }

    public override void DailyEvent()
    {
        throw new System.NotImplementedException();
    }

    public override string SubTypeToString()
    {
        return "this is not useful I believe";
    }
}
