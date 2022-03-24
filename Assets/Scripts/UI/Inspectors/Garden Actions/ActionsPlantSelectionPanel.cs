using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;

public class ActionsPlantSelectionPanel : Singleton<ActionsPlantSelectionPanel>
{
    [SerializeField] ActionsPlantSelectionPanelButton _buttonPrefab;

    private void Update()
    {
        foreach (Plant item in PlantFactory.GetPlantTypes())
        {
            ActionsPlantSelectionPanelButton clone = Instantiate(_buttonPrefab, transform);            
        }
    }
}
