using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : Singleton<Garden>
{
    public GardenTile selectedGardenTile { get; private set; }

    public void SetSelectedGardenTile(GardenTile gardenTile)
    {        
        if(selectedGardenTile == gardenTile)
        {
            InspectorGarden.RefreshPanel();
            ActionsGardenPanel.RefreshPanel();
        }
        selectedGardenTile = gardenTile;
        Debug.Log(selectedGardenTile);
        InspectorGarden.SetActive();
        ActionsGardenPanel.SetActive();
    }
    
    
    

    
    

    
        
}