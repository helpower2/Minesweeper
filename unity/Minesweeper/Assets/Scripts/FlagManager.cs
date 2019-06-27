using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : Singleton<FlagManager>
{

    [SerializeField] private int FlagCount = 10;
    public Text flagcountText;

    public void IncreaseFlagCount(int IncreaseFlag = 1)
    {
        FlagCount += IncreaseFlag;

        flagcountText.text = "" + FlagCount;
    }
    public void Restart()
    {
        GameManager.Instance().won = true;

        if(GameManager.Instance().won == true)
        {
            GetComponent<ClickManager>().enabled = true;
            GetComponent<ClickManagerLeft>().enabled = true;
        }
        FlagCount = GenarateMap.Instance().bombCount;
    }
    public void Start()
    {
        Restart();
        flagcountText.text = "" + FlagCount;
    }
}
