using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Calendar Date Object", menuName = "Gardening Game/Calendar Date Object")]
public class CalendarDate : ScriptableObject
{
    [Range(1,30)]public int day = 30;
    [Range(1, 12)] public int month = 3;
    [Range(1, 999)] public int year = 1;

    public Action actionOnGameTickAdvance;
    public Action actionOnCalendarDayAdvance;
    public bool IsTestMode = false;

    public string GetDate(int format = 0) => format switch
    {
        1 => day.ToString("D2") + "/" + month.ToString("D2") + "/" + year.ToString("D3"),
        2 => year.ToString("D2") + "/" + month.ToString("D2") + "/" + day.ToString("D3"),
        _ => month.ToString("D2") + "/" + day.ToString("D2") + "/" + year.ToString("D3")
    };

    public override string ToString() => GetDate();

    public void AdvanceTime() => actionOnGameTickAdvance?.Invoke();
    public void AdvanceCalendar()
    {
        if (IsTestMode)
        {
            actionOnCalendarDayAdvance?.Invoke();
            return;
        }

        if (day + 1 > 30)
        {
            if (month + 1 > 12)
            {
                year++;
                month = 1;
                day = 1;
            }
            else
            {
                month++;
                day = 1;
            }
        }
        else
        {
            day++;
        }
        actionOnCalendarDayAdvance?.Invoke();
        //Debug.Log(GetDate());
    }

    public void SetCalendarDate(int d, int m, int y)
    {
        day = d;
        month = m;
        year = y;
    }
}
