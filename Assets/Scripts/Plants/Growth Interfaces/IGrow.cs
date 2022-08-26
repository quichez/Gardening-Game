// IGrow - the interfaces for initial plant growth
namespace GardeningGame.Plants
{
    public interface IGrowFromSeed
    {
        bool IsSeed { get; }
        int seedsToPlant { get; }
        int seedsOnHarvest { get; }
        /*int daysToGerminate { get; }
        float minimumTemperatureToGerminate { get; }
        float moistureRequiredForGermination { get; }
        int daysAtGerminationConditions { get; }
        bool IsGerminated => daysToGerminate <= daysAtGerminationConditions;
        bool CountNewDayTowardsGermination();

        int daysToFirstLeaves { get; }
        int daysAsSeedling { get; }
        bool HasFirstLeaves => daysAsSeedling >= daysToFirstLeaves;
        bool CountNewDayTowardsFirstLeaves();
        */
    }

    public  interface IGrowFromCutting
    {
        bool IsCutting { get; }
        bool HasRoots { get; }
    }

    /*public interface IGrowFromRhizome
    {
        int IsRhizomeDormant { get; }
        float tempReqForRhizomeAwake { get; }
        int daysReqForRhizomeAwake { get; }
    }*/

    /*public interface IGrowFromSpore
    {
        bool IsSpore { get; }
        int daysToSproutFromSpore { get; }
        bool IsSporeSprouted { get; }
    }*/

}
