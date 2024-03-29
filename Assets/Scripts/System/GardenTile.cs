using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GardeningGame.Plants;

public class GardenTile : MonoBehaviour
{
    [SerializeField] SpriteRenderer _moistureMask, _plantSprite, _selectMask;
   
    public Plant plant;
    public string nameOfPlant;
    public bool IsSoilPlanted => plant != null;

    #region Soil Quality
    [Header("Soil Quality")]
    [Tooltip("Current soil moisture.")]
    [Range(-1.0f,2.0f)] public float soilMoisture = 0.0f;

    [Tooltip("Cultivating the soil keeps it young. Measured in days.")]
    public int soilAge { get; private set; }

    [Tooltip("Soil with better drainage dry out faster.")]
    [SerializeField] float soilDrainage;

    [Tooltip("Coarse soil, like dirt, or fine soil, like sand.")]
    [SerializeField] float soilTexture;

    [Tooltip("Some plants like more alkaline soils, others more acidic. Measured in pH")]
    [SerializeField] float soilAcidity;
   
    // These depend on GardenSettings
    float soilMoistureMinimum => -0.3f + 1 * Weather.Instance.currentHumidity;
    float soilMoistureMaximum = 2.0f;
    #endregion

    #region Expand Nutrients
    [Tooltip("Nutrients")]
    public float nitrogen { get; private set; } = 0.1f;
    public float phosphorus { get; private set; } = 0.2f;
    public float potassium { get; private set; } = 0.3f;

    public float calcium { get; private set; }
    public float sulfur { get; private set; }
    public float magnesium { get; private set; }

    public float copper { get; private set; }
    public float manganese { get; private set; }
    public float iron { get; private set; }
    public float zinc { get; private set; }
    public float molybdenum { get; private set; }
    public float boron { get; private set; }
    #endregion

    private void Start()
    {
        Calendar.Instance.SubscribeToDailyEvents(DailyEvent);
        nameOfPlant = plant?.plantName;
        _plantSprite.sprite = plant?.sprite;
    }    

    public void DailyEvent()
    {
        DailyPlantCheck();
        DailyMoistureLoss();
        DailySoilAge();
    }

    public void PlantInCell(Plant plant)
    {
        this.plant = plant;        
        nameOfPlant = plant.plantName;
        _plantSprite.sprite = plant.sprite;
        
        switch (plant)
        {
            case IGrowFromSeed seed:
                if (Inventory.Instance.RemoveItem(seed.seedType,seed.seedType.quantity))
                {
                    break;
                }
                else
                {
                    Money.Instance.RemoveFromBalance(plant.cost);
                    break;
                }
            default:
                break;
        }
        InspectorGarden.Instance.SetActiveInspectorPlant();
    }

    private void DailyMoistureLoss()
    {
        if(soilMoisture > 0.0f)
        {
            soilMoisture -= 0.2f * (1 - Weather.Instance.currentHumidity);
        }
        else
        {
            soilMoisture -= 0.2f * (0.8f - Weather.Instance.currentHumidity);
        }

        soilMoisture = Mathf.Clamp(soilMoisture, soilMoistureMinimum, soilMoistureMaximum);
        SetMoistureMaskAlpha();
    }

    private void SetMoistureMaskAlpha()
    {
        float a = (soilMoisture - soilMoistureMaximum) / (soilMoistureMinimum - soilMoistureMaximum);
        _moistureMask.color = new Color(_moistureMask.color.r, _moistureMask.color.g, _moistureMask.color.b, a);
    }

    private void DailySoilAge()
    {
        soilAge += 1;
    }

    private void DailyPlantCheck()
    {
        if (plant == null) return;
        plant.DailyEvent();
        Debug.Log(plant.StageToString());
        Debug.Log((plant as Annual).IsGerminated);
        Debug.Log(plant.sprite);
        plant.CheckSoilConditions(this);
        plant.CheckWeatherConditions();    
        
        _plantSprite.sprite = plant.GetSprite();
    }

    public void WaterGardenTile()
    {
        soilMoisture += Mathf.Max(0,(2.0f - soilMoisture)) * 0.2f;
        SetMoistureMaskAlpha();
    }

    public void ClearGardenTile()
    {
        InspectorGarden.Instance.SetActiveInspectorPlant(false);

        soilMoisture = 0.3f;
        soilTexture += 0.1f;
        soilDrainage += 0.2f;

        plant.OnHarvest();

        plant = null;
        nameOfPlant = null;
        _plantSprite.sprite = null;

    }

    public void TillSoil()
    {
        soilAge = 0;
        soilTexture += 0.2f;
        soilDrainage += 0.3f;
    } 

    private void OnMouseUpAsButton()
    {
        Garden.Instance.SetSelectedGardenTile(this);        
    }

    public void SetSelectMaskActive(bool active = true) => _selectMask.gameObject.SetActive(active);
}


