using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace GardeningGame.Plants
{
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
        public Plant()
        {
            tile = Garden.Instance.selectedGardenTile;
        }

        public Plant(GardenTile gardenTile) { tile = gardenTile; }

        public void TakeDamage(int amt = 1)
        {
            health = Mathf.Max(0, health - amt);
        }

        public abstract void CheckSoilConditions(GardenTile gardenTile);

        public abstract void CheckWeatherConditions();

        public override string ToString() => plantName;

        public virtual string SubTypeToString()
        {
            return "Subtypes not defined.";
        }

        public abstract Sprite GetSprite();

        public virtual void DailyEvent()
        {
            CheckSoilConditions(tile);
            CheckWeatherConditions();
            age++;
            //Debug.Log(sprite.name);
        }

        public virtual void OnPlant()
        {
            Debug.Log("I was planted!");
        }

        public virtual string StageToString() => "base";
    }

    public interface IAnnual
    {
        AnnualSeeds seedType { get; }
        System.Type seedType1 { get; }
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
