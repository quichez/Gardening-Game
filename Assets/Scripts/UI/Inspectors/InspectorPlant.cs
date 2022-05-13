using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GardeningGame.Plants;

public class InspectorPlant : Singleton<InspectorPlant>
{
    [SerializeField] TextMeshProUGUI _plantName;
    [SerializeField] TextMeshProUGUI _plantTypeDescription;
    [SerializeField] TextMeshProUGUI _plantStage;

    private void Update()
    {
        _plantName.text = Garden.Instance.selectedGardenTile.plant.plantName;
        _plantTypeDescription.text = Garden.Instance.selectedGardenTile.plant.SubTypeToString();
        _plantStage.text = Garden.Instance.selectedGardenTile.plant.StageToString();
    }
}
