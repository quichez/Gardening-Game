using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;

public class Crabgrass : Plant
{
    public override string plantName => "Crabgrass";

    public override string description => "Not that kind.";

    public override Sprite sprite => Resources.Load<Sprite>("Sprites/TestPlants/Weed");

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
        if(Weather.Instance.currentTemperature < -10.0f)
        {
            TakeDamage(10);
        }
    }

    public override string SubTypeToString() => "Weed";

    public Crabgrass() { }
}
