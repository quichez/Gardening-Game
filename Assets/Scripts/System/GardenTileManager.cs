using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenTileManager : Singleton<GardenTileManager>
{
    public Vector2Int gardenDimensions = new(1, 1);
    [SerializeField] GardenTile _gardenTilePrefab;
    [SerializeField] Transform _tileParent;

    private void Start()
    {
        for (int i = 0; i < gardenDimensions.y; i++)
        {
            for (int j = 0; j < gardenDimensions.x; j++)
            {
                GardenTile clone = Instantiate(_gardenTilePrefab, _tileParent);
                clone.transform.localPosition = new Vector3(-0.5f + ((float)(j+1)/(gardenDimensions.x + 1)), -0.5f + ((float)(i + 1) / (gardenDimensions.y + 1)),-0.01f);
                if (i == 1 && j == 1) clone.PlantInCell(new Crabgrass());
                if (i == 0 && j == 0) clone.PlantInCell(new TestPlant());
            }
        }
    }
}
