using UnityEngine;
using TMPro;
using System;

//This script is where the time will be kept and monitored. This is where I will put the tracking for daily goals

public class TimeSystem : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public int dayOfWeekInt;
    private SceneSystem scene;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        scene = FindObjectOfType<SceneSystem>();
        
        UpdateTime();
    }

    void Update()
    {
        UpdateTime(); // time continues to update
    }

    void UpdateTime()
    {
        string currentTime = DateTime.Now.ToString("HH:mm:ss"); // put it in format
        string dayOfWeek = DateTime.Now.DayOfWeek.ToString(); // days of the week for streaks

        textMeshPro.text = "Time: " + currentTime + "\nWeekly: " + dayOfWeek + "\nMoney: " + scene.money;

        switch(DateTime.Now.DayOfWeek)
        {
        case DayOfWeek.Monday:
            dayOfWeekInt = 0;
            break;
        case DayOfWeek.Tuesday:
            dayOfWeekInt = 1;
            break;
        case DayOfWeek.Wednesday:
            dayOfWeekInt = 2;
            break;
        case DayOfWeek.Thursday:
            dayOfWeekInt = 3;
            break;
        case DayOfWeek.Friday:
            dayOfWeekInt = 4;
            break;
        case DayOfWeek.Saturday:
            dayOfWeekInt = 5;
            break;
        case DayOfWeek.Sunday:
            dayOfWeekInt = 6;
            break;
        }
    }
}