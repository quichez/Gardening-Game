using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActionsTilePanel : Singleton<ActionsTilePanel>
{

    [SerializeField] Button _plantButton, _waterButton, _tillButton, _clearButton;

    private void OnEnable()
    {
        _plantButton.interactable = !Garden.Instance.selectedGardenTile.IsSoilPlanted;
        _clearButton.interactable = Garden.Instance.selectedGardenTile.IsSoilPlanted;
    }

    public void OpenPlantMenu()
    {
        ActionsGardenPanel.Instance.ToggleActionPanelPlantSelection();
    }

    public void WaterSelectedTile()
    {
        Garden.Instance.selectedGardenTile.WaterGardenTile();
    }

    public void TillSelectedTile()
    {
        Garden.Instance.selectedGardenTile.TillSoil();
    }

    public void ClearSelectedTile()
    {
        Garden.Instance.selectedGardenTile.ClearGardenTile();
    }
}
