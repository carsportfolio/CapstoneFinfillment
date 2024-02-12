using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angelfish : MonoBehaviour
{
    private string fishName;
    private Color color;
    private string goal;
    private int dailyStreak;
    private int totalStreak;
    private Angelfish(string angelfishName, string angelfishGoal)
    {
        fishName = angelfishName;
        color = new Color(1f, 0.5f, 0f); // orange
        goal = angelfishGoal;
        dailyStreak = 0;
        totalStreak = 0;
    }

    public static Angelfish CreateAngel(string angelfishName, string angelfishGoal)
    {
        return new Angelfish(angelfishName, angelfishGoal);
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
