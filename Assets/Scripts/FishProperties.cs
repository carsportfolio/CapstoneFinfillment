using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishProperties : MonoBehaviour
{
    public enum FishType
    {
        Goldfish,
        Betafish,
        Angelfish
    }
    public enum AchievementCategory
    {
        Mental,
        Physical,
        Productivity
    }
    public string Name { get; set; }
    public string Goal { get; set; } // string typed from user of their goal
    public int StreakTracked { get; set; } // per week how many days of the week we track out of it, non-concurrent days
    public int TotalDaysTracked { get; set; } // total tracked EVER - nice for achievement eventually
    public string AchievementsType { get; set; } // of the three categories
    public bool Tracked { get; set; } // for the day, if you tracked to prevent retracking  
    public GameObject FishPrefab { get; set; }
    public DateTime lastPressTime { get; set; } // tracking pressing
}
