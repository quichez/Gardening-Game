using UnityEngine;

namespace GardeningGame
{
    namespace Plants
    {
        public enum FlowerStage { Germinating, Sprouting, Growing, Budding, Flowering, Seeding, Wilting}




        public abstract class Plant
        {
            public abstract string plantName { get; }
            public abstract string description { get; }
            public abstract Sprite sprite { get; }

            public int health = 100;
            public bool IsDead => health == 0;

            public Plant() { }            

            public void TakeDamage(int amt = 1)
            {
                health = Mathf.Max(0, health - amt);                
            }

            public abstract void CheckSoilConditions(GardenTile gardenTile);

            public override string ToString() => plantName;

            public abstract string SubTypeToString();
        }

        public abstract class Flower : Plant
        {
            public override string SubTypeToString() => "Flower";
        }

        public abstract class Weed : Plant
        {
            public override string SubTypeToString() => "Weed";
        }

        public class Tulip : Flower
        {
            public override string plantName => "basic tulip";
            public override string description => "no viruses here.";

            public override Sprite sprite => Resources.Load<Sprite>("Sprites/TestPlants/Weed");

            public override void CheckSoilConditions(GardenTile gardenTile)
            {
                throw new System.NotImplementedException();
            }
        }

        public interface IFlower
        {
            FlowerStage flowerStage { get; set; }
            int daysToGerminate { get; }
            int daysToSprout { get; }
            int daysToFullSize { get; }
            int daysToBud { get; }
            int daysToBloom { get; }
            int daysToSetSeed { get; }
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