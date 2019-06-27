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
        FlagCount = GenarateMap.Instance().bombCount;
    }
    public void Start()
    {
        Restart();
        flagcountText.text = "" + FlagCount;
    }
}
