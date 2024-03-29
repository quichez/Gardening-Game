using UnityEngine;
using UnityEngine.U2D;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace GardeningGame
{
    namespace Plants
    {
        
        public abstract class Plant : IDailyEvent
        {
            public abstract string plantName { get; }
            public abstract string description { get; }
            public abstract int cost { get; }
            public int age { get; private set; }
            public Sprite sprite { get; private set; }
            public abstract SpriteAtlas atlas { get; }
            public int health { get; private set; } = 100;
            public bool IsDead => health == 0;
            public readonly GardenTile tile;

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

            public abstract string SubTypeToString();

            public abstract Sprite GetSprite();

            public virtual void DailyEvent()
            {
                CheckSoilConditions(tile);
                CheckWeatherConditions();
                age++;                
                //Debug.Log(sprite.name);
            }

            public virtual string StageToString() => "base";

            public virtual void OnHarvest()
            {
                Debug.Log("Harvested!");
            }
        }

        public static class PlantFactory
        {
            private static Dictionary<string, Type> _plantByName;
            private static bool IsInitialized => _plantByName != null;

            private static void InitializeFactory()
            {
                if (IsInitialized) return;

                var plantTypes = Assembly.GetAssembly(typeof(Plant)).GetTypes()
                    .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Plant)));

                _plantByName = new Dictionary<string, Type>();

                foreach (var type in plantTypes)
                {
                    if (Activator.CreateInstance(type) is Plant temp) _plantByName.Add(temp.plantName, type);
                }
            }

            public static Plant GetPlant(string plantType)
            {
                InitializeFactory();
                if (!_plantByName.ContainsKey(plantType)) throw new ArgumentException("The plant " + plantType + " has not been created!");

                var type = _plantByName[plantType];
                var plant = Activator.CreateInstance(type) as Plant;
                return plant;
            }

            public static List<Plant> GetPlantTypes()
            {
                InitializeFactory();
                List<Plant> temp = new List<Plant>(_plantByName.Count);
                foreach (var item in _plantByName.Values)
                {
                    temp.Add(Activator.CreateInstance(item) as Plant);
                }
                return temp;
            }

            public static IEnumerable<string> GetResourceNames()
            {
                InitializeFactory();
                return _plantByName.Keys;
            }
        }

        public abstract class Annual : Plant, IGrowFromSeed
        {
            public Annual() { }
            public Annual(GardenTile gardenTile) : base(gardenTile) {}
            public abstract Seed seedType { get; }
            public abstract int daysToGerminate { get; }
            public abstract float minimumTemperatureToGerminate { get; }
            public abstract float moistureRequiredForGermination { get; }
            public abstract float moistureToleranceForGermination { get; }
            public int daysAtGerminationConditions { get; private set; }


            public abstract int daysToFirstLeaves { get; }
            public int daysAsSeedling { get; private set; }
            
            public abstract int daysToMaturity { get; }
            public int daysOfVegetativeGrowth { get; private set; }

            public bool IsGerminated => daysAtGerminationConditions >= daysToGerminate;
            public bool HasFirstLeaves => daysAsSeedling >= daysToFirstLeaves;
            public bool IsMature => daysOfVegetativeGrowth >= daysToMaturity;

            public override void DailyEvent()
            {
                base.DailyEvent();
                
                if (!CountNewDayTowardsGermination())
                {
                    if (!CountNewDayTowardsFirstLeaves())
                    {
                        CountNewDayTowardsMaturity();
                    }
                }
            }

            public virtual bool CountNewDayTowardsGermination()
            {
                if (IsGerminated || IsDead) return false;
                Debug.Log(Weather.Instance.currentTemperature >= minimumTemperatureToGerminate && Mathf.Abs(tile.soilMoisture - moistureRequiredForGermination) < 0.3f);
                if (Weather.Instance.currentTemperature >= minimumTemperatureToGerminate && Mathf.Abs(tile.soilMoisture - moistureRequiredForGermination) < 0.3f)
                {
                    daysAtGerminationConditions++;
                }
                Debug.Log(daysAtGerminationConditions);
                return true;
            }

            public virtual bool CountNewDayTowardsFirstLeaves()
            {
                if (!IsGerminated  || HasFirstLeaves || IsDead) return false;
                daysAsSeedling++;
                return true;
            }

            public virtual bool CountNewDayTowardsMaturity()
            {
                if (!HasFirstLeaves || IsMature || IsDead) return false;
                daysOfVegetativeGrowth++;
                return true;
            }

            public override string StageToString()
            {
                return IsDead ? "Dead" : IsGerminated ? HasFirstLeaves ? IsMature ? "Mature" : "Growing" : "Seedling" : "Germinating";
            }
        }

        public interface IFlower
        {

        }

        public interface IFruit : IFlower
        {

        }

        public interface IHarvestable
        {

        }

        public interface IHarvestPlant : IHarvestable
        {

        }

        public interface IHarvestFruit<T> : IHarvestable where T: IFruit
        {

        }

        public interface IHarvestSeed<T> : IHarvestable where T : IGrowFromSeed
        {

        }

        public interface ISpreadToSurroundingTile
        {

        }

        public interface IPollinate
        {

        }

        public interface ISelfPollinate : IPollinate
        {

        }

        public interface IOtherPollinate : IPollinate
        {

        }
    }

    namespace NutrientInfo
    {
        public struct NutrientInformation
        {
            public string nutrient { get; }
            public string description { get; }

            public NutrientInformation(string nutrient, string desc)
            {
                this.nutrient = nutrient;
                description = desc;
            }
        }
    }

    namespace Inspectors
    {
        public abstract class Inspector : MonoBehaviour
        {
            public abstract void FillInspector();
            public abstract void ClearInspector();
        }
    }

    namespace Items
    {
        public interface IStackable
        {
            int quantity { get; }
            int maxStack { get; }
            bool IsFull => quantity == maxStack;
            void AddToStack(int amount);
            bool RemoveFromStack(int amount);
        }

        public interface IQuality
        {
            int quality { get; }
        }

        public interface IQualityDegrade
        {
            float timeToDegrade { get; }
            float degradeTimer { get; }

            public void AddToDegradeTimer();
        }
    }
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    protected virtual void Awake() => Instance = this as T;

    private void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }

    public static void SetActive(bool active = true) => Instance.gameObject.SetActive(active); 

    public static void RefreshPanelStatic()
    {
        foreach (Transform transform in Instance.transform)
        {
            transform.gameObject.SetActive(false);
            transform.gameObject.SetActive(true);
        }
    }
}

public abstract class Inspector<T> : Singleton<T> where T:MonoBehaviour
{
    public abstract void ActivateInspectors();

    public void TogglePanel(MonoBehaviour panel) => panel.gameObject.SetActive(!panel.gameObject.activeSelf);
}
public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }
}