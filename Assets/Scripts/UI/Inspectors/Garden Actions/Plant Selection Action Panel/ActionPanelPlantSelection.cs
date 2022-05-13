using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;

public class ActionPanelPlantSelection : Singleton<ActionPanelPlantSelection>
{
    [SerializeField] ActionPanelPlantSelectionButton _buttonPrefab;

    private void OnEnable()
    {
        foreach (Plant item in PlantFactory.GetPlantTypes())
        {
            ActionPanelPlantSelectionButton clone = Instantiate(_buttonPrefab, transform);
            clone.InitializePlantSelectionButton(item);
        }
    }

    private void OnDisable()
    {
        foreach (Transform transform in transform)
        {
            Destroy(transform.gameObject);
        }
    }

    private void Update()
    {
        if (Garden.Instance.IsSelectedTilePlanted)
        {
            gameObject.SetActive(false);
        }
    }
}
