using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : Singleton<Weather>
{
    [SerializeField] ClimateSettings _gardenSettings;
    public float currentTemperature { get; private set; }
    public float currentHumidity { get; private set; }

    public bool IsHeatWave { get; private set; }
    public bool IsColdSnap { get; private set; }
    public bool IsStormFront { get; private set; }
    public bool IsDrought { get; private set; }

    private void Start()
    {
        Calendar.Instance.SubscribeToDailyEvents(ChanceOfWeatherPhenomenon);
        Calendar.Instance.SubscribeToGameTickEvents(SetCurrentTemperature);
        Calendar.Instance.SubscribeToGameTickEvents(SetCurrentHumidity);
        ChanceOfWeatherPhenomenon();
        SetCurrentTemperature();
        SetCurrentHumidity();
    }

    public void ChanceOfWeatherPhenomenon()
    {
        ChanceForHeatWave();
        ChanceForColdSnap();
        ChanceForStormFront();
        ChanceForDrought();
    }

    public void ChanceForHeatWave()
    {
        if (!IsHeatWave)
        {
            if (IsColdSnap) return;
            if (Random.value > (0.99f - 0.05f * Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings)))
            {
                IsHeatWave = true;
            }
        }
        else
        {
            if (Random.value > (0.7f + 0.05f * Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings)))
            {
                IsHeatWave = false;
            }
        }
    }

    public void ChanceForColdSnap()
    {
        if (!IsColdSnap)
        {
            if (IsHeatWave) return;
            if (Random.value > (0.9f + 0.2f * Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings)))
            {
                IsColdSnap = true;
            }
        }
        else
        {
            if (Random.value > (0.3f - 0.6f * Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings)))
            {
                IsColdSnap = false;
            }
        }
    }

    public void ChanceForStormFront()
    {
        if (!IsStormFront)
        {
            if (IsDrought)
            {
                if (Random.value > (0.99f - 0.01f * (-0.5f + Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings))))
                {
                    IsStormFront = true;
                    IsDrought = false;
                }
            }
            else
            {
                if (Random.value > (0.80f - 0.1f * (-0.5f + Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings))))
                {
                    IsStormFront = true;
                }
            }
            
        }
        else
        {
            if (Random.value > (0.5f - 0.1f * (-0.5f + Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings))))
            {
                IsStormFront = false;
            }
        }
    }

    public void ChanceForDrought()
    {
        if (!IsDrought)
        {
            if (IsStormFront)
            {
                if (Random.value > (0.99f - 0.01f * (-0.5f + Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings))))
                {
                    IsDrought = true;
                    IsStormFront = false;
                }
            }
            else
            {
                if (Random.value > (0.80f - 0.1f * (-0.5f + Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings))))
                {
                    IsDrought = true;
                }
            }

        }
        else
        {
            if (Random.value > (0.3f - 0.1f * (-0.5f + Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings))))
            {
                IsDrought = false;
            }
        }
    }

    private void SetCurrentTemperature()
    {
        Vector2 lows = _gardenSettings.monthlyLowTempRange;
        Vector2 highs = _gardenSettings.monthlyHighTempRange;

        //Get current base point on yearly ranges
        float lowVal = Mathf.Lerp(lows.x, lows.y, Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings));
        float highVal = Mathf.Lerp(highs.x, highs.y, Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings));

        float dailyCycleVal = -5.0f + 10.0f*Calendar.Instance.GetCosValueForDailyCycle(0.5f)+5.0f * Random.value;


        float currentTemp = Mathf.Lerp(lowVal, highVal, 0.5f) + dailyCycleVal;
        currentTemp = IsHeatWave ? currentTemp + 10 : currentTemp;
        currentTemp = IsColdSnap ? currentTemp - 10 : currentTemp;

        float wholeTemp = Mathf.Floor(currentTemp);
        float decimalPart = currentTemp - Mathf.Floor(currentTemp);
        float decimalPartRounded = Mathf.Floor(decimalPart * 10.0f) / 10.0f;

        currentTemperature = wholeTemp + decimalPartRounded;
    }

    /// <summary>
    /// Calculate and return the current humidity in the garden.
    /// </summary>
    /// <returns> Current humidity between 0.0 and 1.0.</returns>
    private void SetCurrentHumidity()
    {
        Vector2 hRange = _gardenSettings.monthlyHumidityRange;
        float curHumidity = Mathf.Lerp(hRange.x, hRange.y, Calendar.Instance.GetCosValueFromSeasonalCycle(_gardenSettings) - 0.05f * (Random.value - 0.5f));        
        curHumidity = IsStormFront ? IsColdSnap ? Mathf.Min(0.6f,curHumidity + 0.2f) : Mathf.Min(1.0f, curHumidity + 0.6f) : IsDrought ? curHumidity / 2.0f : curHumidity;
        curHumidity = Mathf.Clamp(curHumidity, 0.0f, 1.0f);
        //Make this sinusoidal at some point
        currentHumidity = curHumidity;
    }
}
