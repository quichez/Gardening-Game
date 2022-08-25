using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IGrow - the interfaces for initial plant growth
namespace GardeningGame.Plants
{
    public interface IGrowFromSeed
    {
        Seed seedType { get; }
        int daysToGerminate { get; }
        float minimumTemperatureToGerminate { get; }
        float moistureRequiredForGermination { get; }
        int daysAtGerminationConditions { get; }
        bool IsGerminated => daysToGerminate <= daysAtGerminationConditions;
        bool CountNewDayTowardsGermination();

        int daysToFirstLeaves { get; }
        int daysAsSeedling { get; }
        bool HasFirstLeaves => daysAsSeedling >= daysToFirstLeaves;
        bool CountNewDayTowardsFirstLeaves();

    }

    public interface IGrowFromRhizome
    {
        int IsRhizomeDormant { get; }
        float tempReqForRhizomeAwake { get; }
        int daysReqForRhizomeAwake { get; }
    }

    public interface IGrowFromSpore
    {
        int daysToSproutFromSpore { get; }
        bool IsSporeSprouted { get; }
    }

}
