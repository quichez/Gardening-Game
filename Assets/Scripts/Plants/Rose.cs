using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;

public class Rose : Flower, IPerennial
{
    public PerennialStage perennialStage { get; set; } = PerennialStage.Awake;
    public int age { get; private set; } = 0;

    public int lifeExpectancy => 365 * 50;

    public float awakeTemperature => 40.0f;

    public override string plantName => "Rose Bush";

    public override string description => "The prickly flower";

    public override Sprite sprite => Resources.Load<Sprite>("Sprites/TestPlants/Weed");

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
}
