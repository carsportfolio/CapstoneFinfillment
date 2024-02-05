using UnityEngine;
using TMPro;
using System;

//This script is where the time will be kept and monitored. This is where I will put the tracking for daily goals

public class TimeSystem : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        
        UpdateTime();
    }

    void Update()
    {
        UpdateTime(); // time continues to update
    }

    void UpdateTime()
    {
        string currentTime = DateTime.Now.ToString("HH:mm:ss"); // put it in format

        textMeshPro.text = currentTime; // display on TMP
    }
}