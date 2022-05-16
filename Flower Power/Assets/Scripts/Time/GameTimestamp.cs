using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTimestamp
{
    public int year;
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }

    public Season season;

    public enum DayOfTheWeek
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }

    public int day;
    public int hour;
    public int minute;

    //Constructor
    public GameTimestamp(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    // copies timestamp 
    public GameTimestamp(GameTimestamp timestamp)
    {
        this.year = timestamp.year;
        this.season = timestamp.season;
        this.day = timestamp.day;
        this.hour = timestamp.hour;
        this.minute = timestamp.minute;
    }
    public void UpdateClock()
    {
        minute++;

        if(minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if(hour >= 24)
        {
            hour = 0;
            day++;
        }

        if(day > 30)
        {
            day = 1;
            season++;

            if (season == Season.Winter)
            {
                season = Season.Spring;
                year++;
            }
            else
            {
                season++;
            }
        }
    }

    // hours -> minutes
    public static int HoursToMinutes(int hour)
    {
        return hour * 60;
    }

    // days -> hours
    public static int DaysToHours(int days)
    {
        return days * 24;
    }

    // seasons -> days
    public static int SeasonsToDays(Season season)
    {
        int seasonIndex = (int)season;
        
        return seasonIndex * 30;
    }

    // years -> days
    public static int YearsToDays(int years)
    {
        return years * 4 * 30;
    }
    public DayOfTheWeek GetDayOfTheWeek()
    {
        int daysPassed = YearsToDays(year) + SeasonsToDays(season) + day;

        int dayIndex = daysPassed % 7;

        return (DayOfTheWeek)dayIndex;
    }


    // returns the difference between two timestamps
    public static int CompareTimestamps(GameTimestamp ts1, GameTimestamp ts2)
    {
        int ts1Hours = DaysToHours(YearsToDays(ts1.year)) + DaysToHours(SeasonsToDays(ts1.season)) + DaysToHours(ts1.day) + ts1.hour;
        int ts2Hours = DaysToHours(YearsToDays(ts2.year)) + DaysToHours(SeasonsToDays(ts2.season)) + DaysToHours(ts2.day) + ts2.hour;


        return Mathf.Abs(ts2Hours - ts1Hours);
    }
}
