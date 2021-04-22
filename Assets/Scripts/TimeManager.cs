using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private const int Timescale = 600; //  1 second irl = 10 ingame minute (60): 1sec irl = 1 min ingame 
    
    [SerializeField] 
    private Text timeText;
    
    [SerializeField] 
    private Text dayText;
    
    [SerializeField] 
    private Text monthText;
    
    [SerializeField] 
    private Text yearText;
    

    void Update()
    {
        CalculateTime();
        CalculateCalendar();
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
        else if (GameManager.Instance.hours >= 24)
        {
            GameManager.Instance.day++;
            GameManager.Instance.hours = 1;
            CalculateCalendar();
        }
        UpdateDisplay();
    }

    public void CalculateCalendar()
    {
        if (GameManager.Instance.day > 28)
        {
            GameManager.Instance.month++;
            GameManager.Instance.day = 1;
        }
        else if (GameManager.Instance.month > 4)
        {
            GameManager.Instance.year++;
            GameManager.Instance.month = 1;
        }
    }

    public void UpdateDisplay()
    {
        timeText.text = String.Format( "{0:00} : {1:00}"  , GameManager.Instance.hours, GameManager.Instance.minutes);
        dayText.text = "Day: " + (GameManager.Instance.day).ToString();
        monthText.text = "Month:" + (GameManager.Instance.month).ToString();
        yearText.text = "Year: " + (GameManager.Instance.year).ToString();
    }
}
