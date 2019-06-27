using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    //timer met de currentTime in double en een text element om de tijd te updaten en te laten zien
    public Text Timer;
    public double currentTime = 0.00d;

    void Update()
    {
        // de 
        currentTime += 1 * Time.deltaTime;
        Timer.text = currentTime.ToString("000");

    }
}
