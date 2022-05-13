using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;
using UnityEngine.U2D;

public class Crabgrass : Plant
{
    public override string plantName => "Crabgrass";

    public override string description => "Not that kind.";

    public override SpriteAtlas atlas => throw new System.NotImplementedException();

    public override void CheckSoilConditions(GardenTile gardenTile)
    {
        if (gardenTile.nitrogen < 0.2f)
        {
            TakeDamage(4);
        }
        if (gardenTile.soilMoisture < -0.2f)
        {
            TakeDamage(1);
        }
        
    }

    public override string SubTypeToString() => "Weed";

    public override void CheckWeatherConditions()
    {
        if (Weather.Instance.currentTemperature < -10.0f)
        {
            TakeDamage(10);
        }
    }

    public override void DailyEvent()
    {
        
    }

    public override Sprite GetSprite()
    {
        throw new System.NotImplementedException();
    }

    public Crabgrass() { }
    public Crabgrass(GardenTile gardenTile) : base(gardenTile) { }
}
