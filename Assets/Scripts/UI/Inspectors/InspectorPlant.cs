using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GardeningGame.Plants;

public class InspectorPlant : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _plantName;
    [SerializeField] TextMeshProUGUI _plantTypeDescription;

    private void Update()
    {
        _plantName.text = Garden.Instance.selectedGardenTile.plant.plantName;
        _plantTypeDescription.text = Garden.Instance.selectedGardenTile.plant.SubTypeToString();
    }
}
