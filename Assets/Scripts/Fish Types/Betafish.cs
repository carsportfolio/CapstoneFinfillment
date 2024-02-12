using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Betafish : MonoBehaviour
{
    private string fishName;
    private Color color;
    private string goal;
    private int dailyStreak;
    private int totalStreak;

    private Betafish(string betafishName, string betafishGoal)
    {
        fishName = betafishName;
        color = new Color(0f, 0.5f, 1f); // ??
        goal = betafishGoal;
        dailyStreak = 0;
        totalStreak = 0;
    }

    public static Betafish CreateBeta(string betafishName, string betafishGoal)
    {
        return new Betafish(betafishName, betafishGoal);
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
