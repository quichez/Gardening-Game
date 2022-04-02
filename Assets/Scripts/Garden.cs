using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : Singleton<Garden>
{
    public GardenTile selectedGardenTile { get; private set; }
    public bool IsSelectedTilePlanted => selectedGardenTile?.IsSoilPlanted ?? false;
    public bool IsSelectedTileValid => selectedGardenTile != null;
    public void SetSelectedGardenTile(GardenTile gardenTile)
    {
        Debug.Log("hello");
        selectedGardenTile = gardenTile;
        Debug.Log(InspectorGarden.Instance);
        InspectorGarden.Instance.ActivateInspectors();
        ActionsGardenPanel.Instance.ActivateInspectors();
    }
    
    
    

    
    

    
        
}