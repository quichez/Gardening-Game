using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsGardenPanel : Inspector<ActionsGardenPanel>
{
    [SerializeField] ActionsTilePanel _actionsTilePanel;
    [SerializeField] ActionsPlantPanel _actionsPlantPanel;

    public override void ActivateInspectors()
    {
        _actionsTilePanel.gameObject.SetActive(!Garden.Instance.IsSelectedTilePlanted && Garden.Instance.IsSelectedTileValid);
        _actionsPlantPanel.gameObject.SetActive(Garden.Instance.IsSelectedTilePlanted);
    }
}
