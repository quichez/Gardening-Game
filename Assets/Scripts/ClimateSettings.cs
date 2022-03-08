using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Garden Settings",menuName = "Gardening Game/Garden Settings")]
public class ClimateSettings : ScriptableObject
{    
    public GardenHemisphere gardenHemisphere = GardenHemisphere.North;
    public Vector2 lengthOfDayRange = new(9.0f, 15.0f);

    [Header("Temperatures")]
    public Vector2 monthlyHighTempRange = new(20.0f, 80.0f);
    public Vector2 monthlyLowTempRange = new(-20.0f, 60.0f);

    [Header("Precipitation")]
    public Vector2 monthlyPrecipitationRange = new(0.0f, 12.0f);

    [Header("Humidity")]
    [Tooltip("The range of humidity from 0 to 1, with 1 being 100% relativity.")]
    public Vector2 monthlyHumidityRange = new Vector2(0.0f, 1.0f);
}

public enum GardenHemisphere { North, South}
