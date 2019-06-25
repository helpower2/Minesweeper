using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : Singleton<FlagManager>
{

    public int FlagCounter = 10;
    public Text flagcount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        flagcount.text = "" + FlagCounter;
    }

    public void IncreaseFlagCount(int IncreaseFlag = 1)
    {
        FlagCounter += IncreaseFlag;
        flagcount.text = "" + FlagCounter;
    }
}
