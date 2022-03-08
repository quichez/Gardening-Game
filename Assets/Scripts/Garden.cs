using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : Singleton<Garden>
{
    public GardenTile selectedGardenTile { get; private set; }

    public void SetSelectedGardenTile(GardenTile gardenTile)
    {
        selectedGardenTile = gardenTile;
        Debug.Log(selectedGardenTile);
        InspectorGarden.Instance.ActivateInspectors();
        ActionsGardenPanel.Instance.ActivateActionsPanel();
    }
    
    
    

    
    

    
        
}