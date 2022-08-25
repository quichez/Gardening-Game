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
        selectedGardenTile?.SetSelectMaskActive(false);
        selectedGardenTile = gardenTile;
        selectedGardenTile?.SetSelectMaskActive(true);

        InspectorGarden.Instance.ActivateInspectors();
        ActionsGardenPanel.Instance.ActivateInspectors();
        ActionsGardenPanel.Instance.SetActionsTilePanelActive(false);
        
        if(gardenTile != null) ActionsGardenPanel.Instance.SetActionsTilePanelActive(true);
    }
    
    
    

    
    

    
        
}