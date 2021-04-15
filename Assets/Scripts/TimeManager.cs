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

    private double seconds;
    private double minutes;
    private double hours;
    

    // Start is called before the first frame update
    void Start()
    {
        hours = 7;
        minutes = 0;
        seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateTime();
    }

    public void CalculateTime()
    {
        seconds += Time.deltaTime * Timescale;

        if (seconds >= 60)
        {
            minutes++;
            seconds = 0;
        }
        else if (minutes >= 60)
        {
            hours++;
            minutes = 0;
        }
        else if (hours >= 9)
        {
            GameManager.Instance.day++;
            hours = 7;
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
        timeText.text = hours + ":" + minutes;
        dayText.text = "Day: " + (GameManager.Instance.day).ToString();
        monthText.text = "Month:" + (GameManager.Instance.month).ToString();
        yearText.text = "Year: " + (GameManager.Instance.year).ToString();
    }
}
