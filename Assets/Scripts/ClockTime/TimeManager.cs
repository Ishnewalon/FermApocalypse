using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private const int Timescale = 60; 
    
    [SerializeField] 
    private Text timeText;
    
    [SerializeField] 
    private Text dayText;
    
    [SerializeField] 
    private Text monthText;
    
    [SerializeField] 
    private Text yearText;
    
    [SerializeField] 
    private Text moneyText;
    

    void Update()
    {
        CalculateTime();
        CalculateCalendar();
    }

    private void CalculateTime()
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

    private void CalculateCalendar()
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

    private void UpdateDisplay()
    {
        timeText.text = String.Format( "{0:00} : {1:00}"  , GameManager.Instance.hours, GameManager.Instance.minutes);
        dayText.text = "Jour: " + GameManager.Instance.day;
        monthText.text = "Mois:" + GameManager.Instance.month;
        yearText.text = "Année: " + GameManager.Instance.year;
        moneyText.text = GameManager.Instance.PlayerInventory.GetBalance() + "$";
    }
    
}
