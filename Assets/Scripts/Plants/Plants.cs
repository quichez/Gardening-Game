using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace GardeningGame.Plants
{
    public enum FeederType { Heavy, Medium, Light}

    public abstract class Plant : IDailyEvent
    {
        public abstract string plantName { get; }
        public abstract string description { get; }
        public int age { get; private set; }
        public Sprite sprite { get; private set; }
        public abstract SpriteAtlas atlas { get; }
        public int health { get; private set; } = 100;
        public bool IsDead => health == 0;
        public readonly GardenTile tile;

        public bool IsPlanted { get; private set; } = false;

        public abstract FeederType feederType { get; }
        public Plant()
        {
            tile = Garden.Instance.selectedGardenTile;
        }

        public Plant(GardenTile gardenTile) { tile = gardenTile; }

        public void TakeDamage(int amt = 1)
        {
            health = Mathf.Max(0, health - amt);
        }

        public override string ToString() => plantName;

        public virtual string SubTypeToString()
        {
            return "Subtypes not defined.";
        }

        public abstract Sprite GetSprite();

        public void DailyEvent()
        {
            if(!IsDead) age++;
            OnDailyEvent();
        }

        public abstract void OnDailyEvent();

        public abstract void OnPlant();

        public virtual string StageToString() => "base";
    }

    public interface ICheckSoil
    {
        void CheckSoilConditions(GardenTile gardenTile);
    }
    public interface ICheckWeather
    {
        void CheckWeatherConditions();
    }
    public interface IStageSimple 
    { 
    
    }
    public interface IAnnual
    {
        int seedsToPlant { get; }
    }
    public interface IPerennial
    {
        PerennialSeeds seedType { get; }
    }
    public interface IHarvestable
    {
        void OnHarvest();
    }

}
