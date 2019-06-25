using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{

    public Text Timer;
    public double currentTime = 0.00d;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        currentTime += 1 * Time.deltaTime;
        Timer.text = currentTime.ToString("000");

    }
}
