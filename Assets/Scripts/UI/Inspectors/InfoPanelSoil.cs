using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Inspectors;
using TMPro;

public class InfoPanelSoil : Singleton<InfoPanelSoil>
{
    [SerializeField] TextMeshProUGUI _selectedSoilTileText;
    [SerializeField] TextMeshProUGUI _soilPopulatedText;
    [SerializeField] TextMeshProUGUI _soilMoistureText;
    [SerializeField] TextMeshProUGUI _soilAgeText;

    private void OnEnable()
    {
        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        GardenTile curr = Garden.Instance.selectedGardenTile;
        _selectedSoilTileText.text = curr.name;
        _soilPopulatedText.text = curr.IsSoilPlanted.ToString();
        _soilMoistureText.text = GetSoilMoistureStringForInspector(curr);
        _soilAgeText.text = curr.soilAge.ToString();
    }

    public string GetSoilMoistureStringForInspector(GardenTile gardenTile)
    {
        if (gardenTile.soilMoisture < 0.3f) return "Dry";
        if (gardenTile.soilMoisture < 0.6f) return "Normal";
        if (gardenTile.soilMoisture < 1.0f) return "Moist";
        return "Soggy";
    }
}
