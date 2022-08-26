using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Inspectors;

public class InspectorGarden : Inspector<InspectorGarden>
{
    [SerializeField] InfoPanelSoil _inspectorSoil;
    [SerializeField] InspectorPlant _inspectorPlant;

    public override void ActivateInspectors()
    {        
        _inspectorSoil.gameObject.SetActive(Garden.Instance.selectedGardenTile);
        _inspectorPlant.gameObject.SetActive(Garden.Instance.IsSelectedTilePlanted);
    }

    public void SetActiveInspectorPlant(bool active = true)
    {
        _inspectorPlant.gameObject.SetActive(active);
    }
}
