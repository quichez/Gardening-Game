using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsTilePanel : Singleton<ActionsTilePanel>
{
    [SerializeField] ActionsTilePanelButton _buttonPrefab;
    private void OnEnable()
    {
        foreach (Transform transform in transform)
        {
            Destroy(transform.gameObject);
        }

        if (Garden.Instance.IsSelectedTilePlanted)
        {

        }
        else
        {
            ActionsTilePanelButton plantButton = Instantiate(_buttonPrefab, transform);
            plantButton.onClick.AddListener(() => ActionsPlantSelectionPanel.SetActive(!ActionsPlantSelectionPanel.Instance.gameObject.activeSelf));
        }
    }
}
