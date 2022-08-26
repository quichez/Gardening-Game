using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GardeningGame.Plants;
using UnityEngine.U2D;

public class Sunflower : Plant
{
    public override string plantName => "Sunflower";

    public override string description => "Praise the sun!";

    public override SpriteAtlas atlas => Resources.Load<SpriteAtlas>("SpriteAtlases/Annuals/Marigold");

    public override void CheckSoilConditions(GardenTile gardenTile)
    {
        throw new System.NotImplementedException();
    }

    public override void CheckWeatherConditions()
    {
        throw new System.NotImplementedException();
    }

    public override Sprite GetSprite()
    {
        throw new System.NotImplementedException();
    }

    public override string SubTypeToString()
    {
        throw new System.NotImplementedException();
    }
}
