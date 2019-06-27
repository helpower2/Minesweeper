using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    //timer met de currentTime in double en een text element om de tijd te updaten en te laten zien
    public Text Timer;
    public double currentTime = 0.00;
    public bool active = true;
    void Start()
    {
        GameManager gameManager = GameManager.Instance();
        gameManager.OnRestart.AddListener(Restart);
        gameManager.OnWon.AddListener(delegate { setActive(false); } );
    }

    void Update()
    {
        if(active) currentTime += 1 * Time.deltaTime;
        Timer.text = currentTime.ToString("000");

    }

    public void setActive(bool active)
    {
        this.active = active;
    }
    public void Restart()
    {
        currentTime = 0;
        active = true;
    }
}
