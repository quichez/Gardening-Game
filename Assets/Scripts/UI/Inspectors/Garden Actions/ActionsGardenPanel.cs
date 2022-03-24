using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsGardenPanel : Singleton<ActionsGardenPanel>
{
    [SerializeField] ActionsTilePanel _actionsTilePanel;
    [SerializeField] ActionsPlantPanel _actionsPlantPanel;

    public void OnEnable()
    {
        _actionsTilePanel.gameObject.SetActive(true);
        _actionsPlantPanel.gameObject.SetActive(Garden.Instance.selectedGardenTile.IsSoilPlanted);
    }   

    public void TogglePlantSelectionPanel()
    {
        ActionsPlantSelectionPanel.SetActive();
    }
}
