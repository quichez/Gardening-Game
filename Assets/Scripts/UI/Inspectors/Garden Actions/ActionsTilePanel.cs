using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsTilePanel : Singleton<ActionsTilePanel>
{
    [SerializeField] ActionsTilePanelButton _buttonPrefab;
    private void OnEnable()
    {
        if (Garden.Instance.selectedGardenTile.IsSoilPlanted)
        {

        }
        else
        {
            foreach (Transform transform in transform)
            {
                Destroy(transform.gameObject);
            }

            ActionsTilePanelButton plantButton = Instantiate(_buttonPrefab, transform);
            plantButton.onClick.AddListener(() => ActionsPlantSelectionPanel.SetActive(!ActionsPlantSelectionPanel.Instance.gameObject.activeSelf));
        }
    }
}
