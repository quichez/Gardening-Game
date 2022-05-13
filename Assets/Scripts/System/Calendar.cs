using System;
using UnityEngine;

public class Calendar : Singleton<Calendar>
{
    public CalendarDate date;

    public int dayLengthInSeconds = 1;
    float dayTimer = 0.0f;

    public int ticksPerDay = 4;
    int gameTickCounter = 0;

    float percentageThroughDay => dayTimer / dayLengthInSeconds;

    void Update()
    {
        dayTimer += Time.deltaTime;        
        if(dayTimer >= (float)dayLengthInSeconds/ticksPerDay)
        {            
            if (gameTickCounter == 0)
            {
                //Debug.Log(gameTickCounter);
                date.AdvanceCalendar();
            }

            gameTickCounter++;
            dayTimer = 0.0f;
            date.AdvanceTime();
            
            if(gameTickCounter >= ticksPerDay)
            {
                gameTickCounter = 0;
            }

            

        }
    }

    public void LoadCalendarDate(int d, int m, int y)
    {
        date.SetCalendarDate(d, m, y);
        dayTimer = 0.0f;
    }

    public void SubscribeToGameTickEvents(Action gameTickAction) => date.actionOnGameTickAdvance += gameTickAction;
    public void SubscribeToDailyEvents(Action dailyAction)
    {
        date.actionOnCalendarDayAdvance += dailyAction;
    }

    /// <summary>
    /// Get a value from a negative cosine wave representing the circadian cycle.
    /// </summary>
    /// <param name="phaseShift"> The amount to shift the phase of the cosine wave in multiples of pi.</param>
    /// <returns>A float from 0.0 to 1.0, representing 0:00 and 12:00, respectively.</returns>
    public float GetCosValueForDailyCycle(float phaseShift) => -Mathf.Cos(2.0f * Mathf.PI* percentageThroughDay+Mathf.PI/phaseShift)/2.0f + 0.5f;

    /// <summary>
    /// Get a float representing the point in the seasonal cycle, where the cycle starts mid-winter. This is hemisphere-dependent.
    /// </summary>
    /// <returns>A float between 0 and 1, representing the point in the seasonal cycle.</returns>
    private float GetPercentageThroughSeasonalCycle(ClimateSettings climateSettings)
    {
        int month = date.month;

        if (climateSettings.gardenHemisphere == GardenHemisphere.South)
        {
            month = month + 5 % 12 + 1;
        }

        return ((month - 1) * 30 + date.day) / 360.0f;
    }

    /// <summary>
    /// This maps the percentage through the seasonal cycle and maps it to a negative cosine wave.
    /// </summary>
    /// <returns> A value from the first full wavelenght of a cosine wave, where 0 and 1 map to 0, and 0.5 maps to 1.</returns>
    public float GetCosValueFromSeasonalCycle(ClimateSettings climateSettings) => -Mathf.Cos(2.0f * Mathf.PI * GetPercentageThroughSeasonalCycle(climateSettings)) / 2.0f + 0.5f;
}

public interface IGameTickEvent
{
    void GameTickEvent();
}

public interface IDailyEvent
{
    void DailyEvent();
}
