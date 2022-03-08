using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorGarden : Singleton<InspectorGarden>
{
    [SerializeField] InspectorSoil _inspectorSoil;
    [SerializeField] InspectorPlant _inspectorPlant;
    internal void ActivateInspectors()
    {
        _inspectorSoil.gameObject.SetActive(true);
        _inspectorPlant.gameObject.SetActive(Garden.Instance.selectedGardenTile.IsSoilPlanted);
    }
}
