using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsGardenPanel : Inspector<ActionsGardenPanel>
{
    [SerializeField] ActionsTilePanel _actionsTilePanel;
    [SerializeField] ActionsPlantPanel _actionsPlantPanel;
    [SerializeField] ActionPanelPlantSelection _actionPanelPlantSelection;

    public override void ActivateInspectors()
    {
        _actionsTilePanel.gameObject.SetActive(Garden.Instance.IsSelectedTileValid);
        _actionsPlantPanel.gameObject.SetActive(Garden.Instance.IsSelectedTilePlanted);
        _actionPanelPlantSelection.gameObject.SetActive(false);
    }

    public void ToggleActionPanelPlantSelection()
    {
        _actionPanelPlantSelection.gameObject.SetActive(!_actionPanelPlantSelection.gameObject.activeSelf);
    }

    public void SetActionsTilePanelActive(bool active = true)
    {
        _actionsTilePanel.gameObject.SetActive(active);
    }
}
