using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GardeningGame.Plants;

public class ActionPanelPlantSelectionButton : Button
{
    TextMeshProUGUI _text => GetComponentInChildren<TextMeshProUGUI>();
    Plant plant;

    public void InitializePlantSelectionButton(Plant plant)
    {
        this.plant = plant;
        _text.text = plant.plantName;
    }

    public void PutPlantInCell()
    {
        Garden.Instance.selectedGardenTile.PlantInCell(plant);
        ActionsGardenPanel.Instance.SetActionsTilePanelActive(false);
        ActionsGardenPanel.Instance.SetActionsTilePanelActive(true);
    }
}
