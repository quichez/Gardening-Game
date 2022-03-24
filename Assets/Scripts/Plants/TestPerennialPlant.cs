using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;
public class TestPerennialPlant : Plant, IPerennial, IGerminating, IGrowing, IFlowering, ISeeding, IWilting
{
    public override string plantName => "Test Perennial";

    public override string description => "Test Perennial Plant";

    public override Sprite sprite => Resources.Load<Sprite>("Sprites/TestPlants/TestPerennial");

    public PerennialStage perennialStage { get; private set; } = PerennialStage.Awake;

    public int age { get; private set; } = 0;

    public int lifeExpectancy => 365 * 3;

    public float awakeTemperature => 20.0f;

    public float minimumTemperatureToGerminate => 30.0f;

    public int daysAtMinTempRequiredToGerminate => 21;

    public int daysGerminating { get; private set; } = 0;

    public bool IsGerminated => daysGerminating >= daysAtMinTempRequiredToGerminate;

    public int daysToFullSize => 60;

    public int daysGrowing { get; private set; } = 0;

    public bool IsFullSize => daysGrowing >= daysToFullSize;

    public FlowerStage flowerStage { get; private set; } = FlowerStage.Growing;

    public int daysToBlossom => 30;

    public int daysBudding { get; private set; } = 0;

    public bool IsBlossomed => daysBudding >= daysToBlossom;

    public int daysToSeed => 15;

    public int daysSettingSeed { get; private set; } = 0;

    public bool IsSeedHasSet => daysSettingSeed >= daysToSeed;

    public int daysToWilt => 30;

    public int daysWilting { get; private set; } = 0;

    public bool IsWilted => daysWilting >= daysToWilt;

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

    public override string StageToString()
    {
        if (perennialStage == PerennialStage.Dormant) return "Dormant";
        else return flowerStage.ToString();
    }

    public override string SubTypeToString()
    {
        return "this is not useful I believe";
    }
}
