using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private const int Timescale = 180; // 60 1 second irl = 1 ingame minute
    [SerializeField] private Text timeText;
    [SerializeField] private Text dayText;
    [SerializeField] private Text monthText;
    [SerializeField] private Text yearText;
    
    
    void Update()
    {
        CalculateTime();
    }

    public void CalculateTime()
    {
        GameManager.Instance.seconds += Time.deltaTime * Timescale;

        if (GameManager.Instance.seconds >= 60)
        {
            GameManager.Instance.minutes++;
            GameManager.Instance.seconds = 0;
        }
        else if (GameManager.Instance.minutes >= 60)
        {
            GameManager.Instance.hours++;
            GameManager.Instance.minutes = 0;
        }
        else if (GameManager.Instance.hours >= 9)
        {
            GameManager.Instance.day++;
            GameManager.Instance.hours = 7;
            CalculateCalendar();
        }
        UpdateDisplay();
    }

    public void CalculateCalendar()
    {
        if (GameManager.Instance.day > 3)
        {
            GameManager.Instance.month++;
            GameManager.Instance.day = 1;
        }
        else if (GameManager.Instance.month >= 4)
        {
            GameManager.Instance.year++;
            GameManager.Instance.month = 1;
        }
    }

    public void UpdateDisplay()
    {
        timeText.text = GameManager.Instance.hours + ":" + GameManager.Instance.minutes;
        dayText.text = "Day: " + (GameManager.Instance.day).ToString();
        monthText.text = "Month:" + (GameManager.Instance.month).ToString();
        yearText.text = "Year: " + (GameManager.Instance.year).ToString();
    }
}
