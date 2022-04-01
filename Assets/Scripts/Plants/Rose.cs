using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;

public class Rose : Plant
{    public int lifeExpectancy => 365 * 50;

    public float awakeTemperature => 40.0f;

    public override string plantName => "Rose Bush";

    public override string description => "The prickly flower";

    public override Sprite sprite => Resources.Load<Sprite>("Sprites/TestPlants/Weed");

    public Rose(GardenTile gardenTile) : base(gardenTile) { }
    public override void CheckSoilConditions(GardenTile gardenTile)
    {
        Debug.Log("its ok");
    }

    public override void CheckWeatherConditions()
    {
        throw new System.NotImplementedException();
    }

    public override void DailyEvent()
    {
        
    }

    public override string SubTypeToString()
    {
        throw new System.NotImplementedException();
    }
}
