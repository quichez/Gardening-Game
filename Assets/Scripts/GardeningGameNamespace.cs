using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace GardeningGame
{
    namespace Plants
    {
        public enum FlowerStage {Growing, Budding, Flowering, Seeding, Wilting}
        public enum PerennialStage { Awake, Dormant}
        public enum AnnualStage { Alive, Dead}


        public abstract class Plant : IDailyEvent
        {
            public abstract string plantName { get; }
            public abstract string description { get; }
            public abstract Sprite sprite { get; }            
            public int health { get; private set; } = 100;
            public bool IsDead => health == 0;

            public Plant() { }            

            public void TakeDamage(int amt = 1)
            {
                health = Mathf.Max(0, health - amt);                
            }

            public abstract void CheckSoilConditions(GardenTile gardenTile);

            public abstract void CheckWeatherConditions();
            public override string ToString() => plantName;

            public abstract string SubTypeToString();

            public abstract string StageToString();

            public abstract void DailyEvent();
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

        public abstract class Flower : Plant
        {
            public override string SubTypeToString() => "Flower";
            public FlowerStage flowerStage { get; protected set; } = FlowerStage.Growing;

            public int[] stageLengths = new int[7];

            public override string StageToString()
            {
                if(this is IPerennial perennial)
                {
                    if(perennial.perennialStage == PerennialStage.Dormant)
                    {
                        return "Dormant";
                    }
                    else
                    {
                        return flowerStage.ToString();
                    }
                }
                else
                {
                    return flowerStage.ToString();
                }
            }
        }

        public interface IFlowering
        {
            FlowerStage flowerStage { get; }
            int daysToBlossom { get; }
            int daysBudding { get; }
            bool IsBlossomed { get; }
        }

        public interface IPerennial
        {
            PerennialStage perennialStage { get; } 
            int age { get;}
            int lifeExpectancy { get; }
            float awakeTemperature { get; }
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

    public static void RefreshPanel()
    {
        Instance.gameObject.SetActive(false);
        Instance.gameObject.SetActive(true);
    }
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