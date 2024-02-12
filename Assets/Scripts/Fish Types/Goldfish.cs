using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldfish : MonoBehaviour
{
    private string fishName;
    private Color color;
    private string goal;
    private int dailyStreak;
    private int totalStreak;

    private Goldfish(string goldfishName, string goldfishGoal)
    {
        fishName = goldfishName;
        color = new Color(1f, 0.5f, 0f); // orange
        goal = goldfishGoal;
        dailyStreak = 0;
        totalStreak = 0;
    }

    public static Goldfish CreateGold(string goldfishName, string goldfishGoal)
    {
        return new Goldfish(goldfishName, goldfishGoal);
    }

    public Color GetColor()
    {
        return color;
    }

    public string GetName()
    {
        return fishName;
    }

    public string GetGoal()
    {
        return goal;
    }

    public int GetTotalStreak()
    {
        return totalStreak;
    }
    
    public int GetDailyStreak()
    {
        return dailyStreak;
    }
}
