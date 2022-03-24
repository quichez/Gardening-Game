using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;

public class TestPlant : Plant, IGerminating, IGrowing, IFlowering, IWilting, ISeeding
{
    public override string plantName => "Test";

    public override string description => "Test";

    public override Sprite sprite => Resources.Load<Sprite>("Sprites/TestPlants/TestPlant");

    public float minimumTemperatureToGerminate => 0.0f;

    public int daysAtMinTempRequiredToGerminate => 5;
    public int daysGerminating { get; private set; } = 0;
    public bool IsGerminated => daysGerminating >= daysAtMinTempRequiredToGerminate;

    public int daysToFullSize => 1;
    public int daysGrowing { get; private set; } = 0;
    public bool IsFullSize => daysGrowing >= daysToFullSize;

    public FlowerStage flowerStage { get; private set; } = FlowerStage.Growing;
    public int daysToBlossom => 1;
    public int daysBudding { get; private set; } = 0;
    public bool IsBlossomed => daysBudding >= daysToBlossom;

    public int daysToSeed => 1;
    public int daysSettingSeed { get; private set; } = 0;
    public bool IsSeedHasSet => daysSettingSeed >= daysToSeed;

    public int daysToWilt => 1;
    public int daysWilting { get; private set; } = 0;
    public bool IsWilted => daysWilting >= daysToWilt;   

    public override void CheckSoilConditions(GardenTile gardenTile)
    {
        if(gardenTile.nitrogen < 0.1f)
        {
            TakeDamage(1);
        }
    }

    public override void CheckWeatherConditions()
    {
        if (Weather.Instance.currentTemperature < 0.0f)
        {
            TakeDamage(1);
        }
    }

    public override void DailyEvent()
    {
        if (!IsGerminated)
        {
            if(Weather.Instance.currentTemperature >= minimumTemperatureToGerminate)
            {
                daysGerminating++;
            }
        }
        else
        {
            if (!IsFullSize)
            {
                daysGrowing++;
            }
            else
            {
                if (!IsBlossomed)
                {
                    flowerStage = FlowerStage.Budding;
                    daysBudding++;
                }
                else
                {
                    if (!IsSeedHasSet)
                    {
                        flowerStage = FlowerStage.Seeding;
                        daysSettingSeed++;
                    }
                    else
                    {
                        if (!IsWilted)
                        {
                            flowerStage = FlowerStage.Wilting;
                            daysWilting++;
                        }
                        else
                        {
                            TakeDamage(health);
                        }
                    }
                }
            }
        }
    }

    public override string StageToString()
    {
        if (!IsGerminated) return "Germinating";
        if (IsDead) return "Dead";
        else return flowerStage.ToString();
    }

    public override string SubTypeToString()
    {
        return "Test Subtype";
    }

    
}

public interface IGerminating
{
    float minimumTemperatureToGerminate { get; }
    int daysAtMinTempRequiredToGerminate { get; }
    int daysGerminating { get; }
    bool IsGerminated { get; }
}

public interface IGrowing
{
    int daysToFullSize { get; }
    int daysGrowing { get; }
    bool IsFullSize { get; }
}

public interface IWilting
{
    int daysToWilt { get; }
    int daysWilting { get; }
    bool IsWilted { get; }
}

public interface ISeeding
{
    int daysToSeed { get; }
    int daysSettingSeed { get; }
    bool IsSeedHasSet { get; }
}